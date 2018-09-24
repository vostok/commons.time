using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Time
{
    [PublicAPI]
    internal static class TimeSpanArithmetics
    {
        public static TimeSpan Multiply(this TimeSpan time, double multiplier)
        {
            return TimeSpan.FromTicks((long)(time.Ticks * multiplier));
        }

        public static TimeSpan Divide(this TimeSpan time, double divisor)
        {
            return TimeSpan.FromTicks((long)(time.Ticks / divisor));
        }

        public static TimeSpan Abs(this TimeSpan time)
        {
            return time.Ticks >= 0 ? time : time.Negate();
        }

        public static TimeSpan Min(TimeSpan time1, TimeSpan time2)
        {
            return time1 <= time2 ? time1 : time2;
        }

        public static TimeSpan Min(TimeSpan time1, TimeSpan time2, TimeSpan time3)
        {
            return Min(time1, Min(time2, time3));
        }

        public static TimeSpan Max(TimeSpan time1, TimeSpan time2)
        {
            return time1 >= time2 ? time1 : time2;
        }

        public static TimeSpan Max(TimeSpan time1, TimeSpan time2, TimeSpan time3)
        {
            return Max(time1, Max(time2, time3));
        }
    }
}