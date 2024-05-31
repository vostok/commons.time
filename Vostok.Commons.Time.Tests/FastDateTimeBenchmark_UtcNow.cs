using System;
using BenchmarkDotNet.Attributes;

namespace Vostok.Commons.Time.Tests;

public class FastDateTimeBenchmark_UtcNow
{
    [Benchmark]
    public DateTime Vanilla() => DateTime.UtcNow;

    [Benchmark]
    public DateTime Fast() => FastDateTime.UtcNow;
}