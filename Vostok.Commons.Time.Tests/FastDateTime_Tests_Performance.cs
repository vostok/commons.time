using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Validators;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests;

[TestFixture]
internal class FastDateTime_Tests_Performance
{
    /*
// * Summary *

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.19045
11th Gen Intel Core i7-11850H 2.50GHz, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=8.0.205
  [Host] : .NET Core 6.0.30 (CoreCLR 6.0.3024.21525, CoreFX 6.0.3024.21525), 64bit RyuJIT


  Method |      Mean |     Error |    StdDev |
-------- |----------:|----------:|----------:|
 Vanilla | 18.180 ns | 0.1793 ns | 0.1589 ns |
    Fast |  1.880 ns | 0.0062 ns | 0.0049 ns |
     */
    [Test]
    [Explicit]
    public void Benchmark_UtcNow_against_DateTimeOffset()
    {
        BenchmarkRunnerCore.Run(
            BenchmarkConverter.TypeToBenchmarks(typeof(FastDateTimeBenchmark_UtcNow),
                new ManualConfig()
                    .With(InProcessValidator.DontFailOnError, ExecutionValidator.DontFailOnError)
                    .With(DefaultConfig.Instance.GetAnalysers().ToArray())
                    .With(DefaultConfig.Instance.GetLoggers().ToArray())
                    .With(DefaultConfig.Instance.GetColumnProviders().ToArray())
            ),
            job => new InProcessToolchain(true));
    }
}