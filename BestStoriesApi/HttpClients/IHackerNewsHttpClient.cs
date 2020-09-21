using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestStoriesApi.HttpClients
{
    public interface IHackerNewsHttpClient
    {
        /// <summary>
        /// Retrieves the top N best stories ids from hacker news api.
        /// </summary>
        /// <param name="topStories"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetTopNBestStoriesIdsAsync(int topStories = 20);
        /// <summary>
        /// Retrieves a single item, by id.
        /// </summary>
        /// <param name="storyId">The </param>
        /// <returns></returns>
        Task<HackerNewsItem> GetItemAsync(int storyId);
    }
}
