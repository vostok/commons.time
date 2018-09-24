using System;
using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class TimeSpanFormatting_Tests
    {
        [Test]
        public void ToPrettyString_should_correctly_format_days()
        {
            var time = TimeSpan.FromDays(2.5);

            time.ToPrettyString().Should().Be("2.5 days");
            time.ToPrettyString(true).Should().Be("2.5d");
        }

        [Test]
        public void ToPrettyString_should_correctly_format_hours()
        {
            var time = TimeSpan.FromHours(23.5);

            time.ToPrettyString().Should().Be("23.5 hours");
            time.ToPrettyString(true).Should().Be("23.5h");
        }

        [Test]
        public void ToPrettyString_should_correctly_format_minutes()
        {
            var time = TimeSpan.FromMinutes(59.5);

            time.ToPrettyString().Should().Be("59.5 minutes");
            time.ToPrettyString(true).Should().Be("59.5m");
        }

        [Test]
        public void ToPrettyString_should_correctly_format_seconds()
        {
            var time = TimeSpan.FromSeconds(59.5);

            time.ToPrettyString().Should().Be("59.5 seconds");
            time.ToPrettyString(true).Should().Be("59.5s");
        }

        [Test]
        public void ToPrettyString_should_correctly_format_milliseconds()
        {
            var time = TimeSpan.FromMilliseconds(999);

            time.ToPrettyString().Should().Be("999 milliseconds");
            time.ToPrettyString(true).Should().Be("999ms");
        }

        [Test]
        public void ToPrettyString_should_correctly_format_microseconds()
        {
            var time = TimeSpan.FromTicks(10 * 999);

            time.ToPrettyString().Should().Be("999 microseconds");
            time.ToPrettyString(true).Should().Be("999us");
        }
    }
}