using System;

namespace BestStoriesApi.Services
{
    /// <summary>
    /// Best story item.
    /// </summary>
    public class Story
    {
        /// <summary>
        /// title of posts or texts of comments
        /// </summary>
        public string title { get; set; }

        public string uri { get; set; }
        /// <summary>
        /// The username of the item's author.
        /// </summary>
        public string postedBy { get; set; }
        /// <summary>
        /// Datetime offset of the time the entry was added
        /// </summary>
        public DateTimeOffset time { get; set; }
        /// <summary>
        /// The number of points the submission got
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// </summary>
        public int commentCount { get; set; }       
    }
}