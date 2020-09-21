using Newtonsoft.Json;
using System;

namespace BestStoriesApi.HttpClients
{
    /// <summary>
    /// Represents a single item retrieved from hacker news api.
    /// </summary>
    public class HackerNewsItem
    {
        /// <summary>
        /// title of posts or texts of comments
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// The URL of the story.
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// The username of the item's author.
        /// </summary>
        //[JsonProperty("postedBy")]
        public string by { get; set; }
        /// <summary>
        /// UNIX timestamp of the time the entry was added
        /// </summary>       
        public int time { get; set; }
        /// <summary>
        /// only for stories: the number of points the submission got
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// Changed property name to comply to the requirement.
        /// </summary>
        //[JsonProperty("commentCount")]
        public int descendants { get; set; }
        /// <summary>
        /// The item's unique id.
        /// </summary>
        //[JsonIgnore]
        public int id { get; set; }
        /// <summary>
        /// The ids of the item's comments, in ranked display order.
        /// </summary>
        public int[] kids { get; set; }
        /// <summary>
        /// The type of item. One of "job", "story", "comment", "poll", or "pollopt".
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The comment, story or poll text. HTML.
        /// </summary>
        public string text { get; set; }
    }
}
