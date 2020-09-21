using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using BestStoriesApi.HttpClients;
using BestStoriesApi.Services;
using BestStoriesApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Polly;

namespace BestStoriesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApiSettings>(options => Configuration.GetSection(nameof(ApiSettings)).Bind(options));

            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Top 20 best stories.",
                    Description = "Retrieve the top 20 best stories from Hacker News API",
                    Contact = new OpenApiContact
                    {
                        Name = "Jorge Vinagre",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jorgevinagre"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            // Limit the max concurrent requests
            // Extra resilience for high-throughput systems by preventing a faulting downstream system causing an excessive resource bulge of queuing calls upstream.
            int maxParallelism = int.Parse(Configuration.GetSection("ApiSettings:MaxParallelJobs").Value);
            int maxQueueSize = int.Parse(Configuration.GetSection("ApiSettings:MaxQueueItems").Value);

            var throttler = Policy.BulkheadAsync<HttpResponseMessage>(maxParallelization: maxParallelism, maxQueuingActions: maxQueueSize);


            // Add Hacker News baseUri client services as a typed client for HttpClientFactory
            services
                // Add typed client and set its base url
                .AddHttpClient<IHackerNewsHttpClient, HackerNewsHttpClient>(client =>
                {
                    // Set the base address to backoffice client
                    var baseUri = Configuration.GetSection("HackerNews:uri").Value;

                    if (!baseUri.EndsWith("/"))
                    {
                        baseUri = string.Concat(baseUri, "/");
                    }

                    client.BaseAddress = new Uri(baseUri);
                }).AddPolicyHandler(throttler);
            
            services.AddScoped<IBestStoriesService, BestStoriesService>();

            services.AddSingleton<ICacheService, MemoryCacheService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hacker News Best Stories API.");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
