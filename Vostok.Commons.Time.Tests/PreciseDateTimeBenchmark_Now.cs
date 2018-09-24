using System;
using BenchmarkDotNet.Attributes;

namespace Vostok.Commons.Time.Tests
{
    public class PreciseDateTimeBenchmark_Now
    {
        [Benchmark]
        public DateTimeOffset Vanilla() => DateTimeOffset.Now;

        [Benchmark]
        public DateTimeOffset Precise() => PreciseDateTime.Now;
    }
}