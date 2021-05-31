using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Time.TimeProviders
{
    [PublicAPI]
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}