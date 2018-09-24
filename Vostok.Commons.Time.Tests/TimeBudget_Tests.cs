using System;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Commons.Testing;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class TimeBudget_Tests
    {
        [Test]
        public void Infinite_budget_should_not_be_expired_and_have_a_lot_of_remaining_time()
        {
            TimeBudget.Infinite.HasExpired.Should().BeFalse();
            TimeBudget.Infinite.Remaining.Should().Be(TimeSpan.MaxValue);
        }

        [Test]
        public void Expired_budget_should_be_expired_and_have_zero_remaining_time()
        {
            TimeBudget.Expired.HasExpired.Should().BeTrue();
            TimeBudget.Expired.Remaining.Should().Be(TimeSpan.Zero);
        }

        [Test]
        public void StartNew_should_produce_a_running_budget()
        {
            var budget = TimeBudget.StartNew(10.Seconds());

            var remainingBefore = budget.Remaining;

            Thread.Sleep(1);

            budget.Remaining.Should().BeLessThan(remainingBefore);
        }

        [Test]
        public void StartNew_should_produce_a_budget_that_will_eventually_expire()
        {
            var budget = TimeBudget.StartNew(250.Milliseconds());

            budget.HasExpired.Should().BeFalse();

            new Action(() => budget.HasExpired.Should().BeFalse())
                .ShouldPassIn(10.Seconds());
        }

        [Test]
        public void TryAcquire_should_be_limited_by_remaining_time()
        {
            var budget = TimeBudget.StartNew(250.Milliseconds());

            budget.TryAcquire(1.Seconds()).Should().BeLessOrEqualTo(250.Milliseconds());
        }
    }
}