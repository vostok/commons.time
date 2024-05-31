using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests;

[TestFixture]
internal class FastDateTime_Tests
{
    [Test]
    public void Should_not_diverge_from_original_DateTimeOffset()
    {
        var watch = Stopwatch.StartNew();

        while (watch.Elapsed < 10.Seconds())
        {
            FastDateTime.UtcNow.Should().BeCloseTo(DateTime.UtcNow, 100.Milliseconds());
            FastDateTime.Now.Should().BeCloseTo(DateTime.Now, 100.Milliseconds());

            Thread.Sleep(1);
        }
    }

    [Test]
    public void Should_appear_monotonic_if_called_sequentially()
    {
        var watch = Stopwatch.StartNew();
        var previous = FastDateTime.UtcNow;

        while (watch.Elapsed < 10.Seconds())
        {
            Thread.Sleep(1);

            var current = FastDateTime.UtcNow;

            current.Should().BeOnOrAfter(previous);

            previous = current;
        }
    }
}