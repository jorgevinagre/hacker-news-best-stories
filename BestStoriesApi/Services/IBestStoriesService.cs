using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestStoriesApi.Services
{
    public interface IBestStoriesService
    {
        /// <summary>
        /// Retrives the top 20 best stories from hacker news api sorted by score in descending order.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Story>> GetTopTwentyBestStories();
    }
}
