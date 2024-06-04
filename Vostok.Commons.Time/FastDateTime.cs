using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Time
{
    [PublicAPI]
    internal static class FastDateTime
    {
        private static int lastUtcTicks = -1;
        private static DateTime lastDateTimeUtc = DateTime.MinValue;

        private static int lastTicks = -1;
        private static DateTime lastDateTime = DateTime.MinValue;

        /// <summary>        
        /// Gets the current time in an optimized fashion.        
        /// </summary>        
        /// <value>Current time.</value>        

        public static DateTime UtcNow
        {
            get
            {
                // ReSharper disable once RedundantNameQualifier because of ambiguous invocations in projects references vostok.environment and system.environment simultaneously.
                var tickCount = System.Environment.TickCount;
                if (tickCount == lastUtcTicks)
                {
                    return lastDateTimeUtc;
                }

                var dateTimeUtc = DateTime.UtcNow;
                lastUtcTicks = tickCount;
                lastDateTimeUtc = dateTimeUtc;
                return dateTimeUtc;
            }
        }

        public static DateTime Now
        {
            get
            {
                var tickCount = Environment.TickCount;
                if (tickCount == lastTicks)
                {
                    return lastDateTime;
                }

                var dateTime = DateTime.Now;
                lastTicks = tickCount;
                lastDateTime = dateTime;
                return dateTime;
            }
        }
    }
}