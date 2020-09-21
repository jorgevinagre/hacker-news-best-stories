namespace BestStoriesApi.Settings
{
    public class ApiSettings
    {
        public int AbsoluteCacheTimeoutInSecond { get; set; }
        public int TopStoriesCount { get; set; }
        public int MaxParallelJobs { get; set; }
        public int MaxQueueItems { get; set; }
    }
}
