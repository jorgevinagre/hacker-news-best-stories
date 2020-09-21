using BestStoriesApi.HttpClients;
using BestStoriesApi.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestStoriesApi.Services
{
    public class BestStoriesService : IBestStoriesService
    {
        private const string Top20StoriesCacheKey = "TOP_20";
        private readonly IHackerNewsHttpClient httpClient;
        private readonly ICacheService cache;
        private readonly ApiSettings settings;

        public BestStoriesService(IHackerNewsHttpClient httpClient, ICacheService cache, IOptions<ApiSettings> settings)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IEnumerable<Story>> GetTopTwentyBestStories()
        {

            var cachedStories = cache.Get<IEnumerable<Story>>(Top20StoriesCacheKey);

            if (cachedStories != null)
            {
                return cachedStories;
            }

            var tasks = new List<Task<HackerNewsItem>>();

            var top = await httpClient.GetTopNBestStoriesIdsAsync(settings.TopStoriesCount);

            top.ToList().ForEach(s => tasks.Add(httpClient.GetItemAsync(s)));

            var stories = (await Task.WhenAll(tasks)).Select(u => new Story()
            {
                commentCount = u.descendants,
                postedBy = u.by,
                score = u.score,
                time = u.time.ToDateTimeOffset(),
                title = u.title,
                uri = u.url
            });

            cache.Set<IEnumerable<Story>>(Top20StoriesCacheKey, stories, TimeSpan.FromSeconds(settings.AbsoluteCacheTimeoutInSecond));

            return stories;
        }
    }
}
