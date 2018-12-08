using System;
using JetBrains.Annotations;

namespace Vostok.Commons.Time
{
    [PublicAPI]
    internal static class Epochs
    {
        /// <summary>
        /// Gregorian epoch expressed as an UTC <see cref="DateTime"/> instance.
        /// </summary>
        public static readonly DateTime Gregorian = new DateTime(1582, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Unix epoch expressed as an UTC <see cref="DateTime"/> instance.
        /// </summary>
        public static readonly DateTime Unix = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    }
}