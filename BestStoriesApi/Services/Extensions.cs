using System;

namespace BestStoriesApi.Services
{
    public static class Extensions
    {
        public static DateTimeOffset ToDateTimeOffset(this int value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).ToUniversalTime();
        }
    }
}
