using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestStoriesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestStoriesApi.Controllers
{
    [
        Route("api/[controller]"),
        ApiController,
        Produces("application/json")
    ]
    public class BestStoriesController : ControllerBase
    {
        private readonly IBestStoriesService bestStoriesService;

        public BestStoriesController(IBestStoriesService bestStoriesService)
        {
            this.bestStoriesService = bestStoriesService ?? throw new ArgumentNullException(nameof(bestStoriesService));
        }

        /// <summary>
        /// Retrieves the top 20 best stories from hacker news api, in descending order by score.
        /// </summary>
        /// <returns></returns>
        [
            HttpGet,
            Route("top-twenty"),
            ProducesResponseType(typeof(IEnumerable<Story>), 200)
        ]
        public async Task<IActionResult> GetTopTweentyStories()
        {
            return Ok(await bestStoriesService.GetTopTwentyBestStories());
        }
    }
}
