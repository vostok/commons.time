using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Time.TimeProviders
{
    [PublicAPI]
    internal class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}