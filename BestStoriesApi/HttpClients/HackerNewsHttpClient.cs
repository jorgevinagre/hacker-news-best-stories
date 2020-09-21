using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestStoriesApi.HttpClients
{
    public class HackerNewsHttpClient : IHackerNewsHttpClient
    {
        private readonly HttpClient httpClient;

        public HackerNewsHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
      
        public async Task<HackerNewsItem> GetItemAsync(int storyId)
        {
            HackerNewsItem item = null;

            HttpResponseMessage storyResponse = await httpClient.GetAsync($"item/{storyId}.json");
            if (storyResponse.IsSuccessStatusCode)
            {
                var itemResponse = await storyResponse.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<HackerNewsItem>(itemResponse);
            }

            return item;
        }

        public async Task<IEnumerable<int>> GetTopNBestStoriesIdsAsync(int topStories = 20)
        {

            IEnumerable<int> topStoriesIds = Enumerable.Empty<int>();

            HttpResponseMessage response = await httpClient.GetAsync("beststories.json");

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                topStoriesIds = JsonConvert.DeserializeObject<IEnumerable<int>>(responseAsString).Take(topStories);
            }

            return topStoriesIds;
        }       
    }
}
