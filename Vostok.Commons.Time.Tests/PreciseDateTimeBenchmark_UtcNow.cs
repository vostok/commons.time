using System;
using BenchmarkDotNet.Attributes;

namespace Vostok.Commons.Time.Tests
{
    public class PreciseDateTimeBenchmark_UtcNow
    {
        [Benchmark]
        public DateTimeOffset Vanilla() => DateTimeOffset.UtcNow;

        [Benchmark]
        public DateTimeOffset Precise() => PreciseDateTime.UtcNow;
    }
}