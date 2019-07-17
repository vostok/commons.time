using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class PreciseDateTime_Tests_Performance
    {
        // Last results for Stopwatch-based provider:
        //
        // .NET Framework 4.7.1
        //
        //   Method |     Mean |     Error |    StdDev |
        // -------- |---------:|----------:|----------:|
        //  Vanilla | 25.00 ns | 0.1245 ns | 0.1104 ns |
        //  Precise | 42.90 ns | 1.0369 ns | 1.6143 ns |
        //
        // .NET Core 2.1.2
        //
        //   Method |     Mean |     Error |    StdDev |
        // -------- |---------:|----------:|----------:|
        //  Vanilla | 25.60 ns | 0.0480 ns | 0.0401 ns |
        //  Precise | 39.01 ns | 0.4720 ns | 0.4184 ns |
        //
        [Test]
        [Explicit]
        public void Benchmark_UtcNow_against_DateTimeOffset()
        {
            BenchmarkRunnerCore.Run(
                BenchmarkConverter.TypeToBenchmarks(typeof(PreciseDateTimeBenchmark_UtcNow)),
                job => new InProcessToolchain(false));
        }

        // Last results for Stopwatch-based provider:
        //
        // .NET Framework 4.7.1
        //
        //   Method |     Mean |     Error |    StdDev |    Median |
        // -------- |---------:|----------:|----------:|----------:|
        //  Vanilla | 104.5 ns | 10.336 ns | 24.962 ns |  90.24 ns |
        //  Precise | 150.6 ns |  1.793 ns |  1.497 ns | 150.97 ns |
        //
        // .NET Core 2.1.2 
        //
        //   Method |     Mean |     Error |    StdDev |
        // -------- |---------:|----------:|----------:|
        //  Vanilla | 107.8 ns | 0.3219 ns | 0.2688 ns |
        //  Precise | 182.4 ns | 0.9478 ns | 0.8402 ns |
        //
        [Test]
        [Explicit]
        public void Benchmark_Now_against_DateTimeOffset()
        {
            BenchmarkRunnerCore.Run(
                BenchmarkConverter.TypeToBenchmarks(typeof(PreciseDateTimeBenchmark_Now)),
                job => new InProcessToolchain(false));
        }
    }
}