using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class PreciseDateTime_Tests
    {
        [Test]
        public void Should_return_timestamps_with_correct_timezone_offsets()
        {
            PreciseDateTime.UtcNow.Offset.Should().Be(DateTimeOffset.UtcNow.Offset);
            PreciseDateTime.Now.Offset.Should().Be(DateTimeOffset.Now.Offset);
        }

        [Test]
        public void Should_not_diverge_from_original_DateTimeOffset()
        {
            var watch = Stopwatch.StartNew();

            while (watch.Elapsed < 10.Seconds())
            {
                PreciseDateTime.UtcNow.Should().BeCloseTo(DateTimeOffset.UtcNow, 100.Milliseconds());
                PreciseDateTime.Now.Should().BeCloseTo(DateTimeOffset.Now, 100.Milliseconds());

                Thread.Sleep(1);
            }
        }

        [Test]
        public void Should_appear_monotonic_if_called_sequentially()
        {
            var watch = Stopwatch.StartNew();
            var previous = PreciseDateTime.UtcNow;

            while (watch.Elapsed < 10.Seconds())
            {
                var current = PreciseDateTime.UtcNow;

                current.Should().BeAfter(previous);

                previous = current;

                Thread.Sleep(1);
            }
        }
    }
}