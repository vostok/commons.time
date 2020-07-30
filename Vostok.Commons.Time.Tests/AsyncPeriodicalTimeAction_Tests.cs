using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class AsyncPeriodicalTimeAction_Tests
    {
        private int errors;
        private Action<Exception> errorHandler;

        [SetUp]
        public void SetUp()
        {
            errors = 0;
            errorHandler = exception => { ++errors; };
        }

        [Test]
        public void Should_not_stop_when_action_throws_exception()
        {
            var counter = 0;

            Task Action()
            {
                counter++;
                return Task.FromException(new Exception());
            }

            var periodicalAction = new AsyncPeriodicalTimeAction(
                Action,
                errorHandler,
                () => TimeSpan.Zero);

            periodicalAction.Start();

            var spinWait = new SpinWait();
            while (counter < 2)
            {
                spinWait.SpinOnce();
            }

            periodicalAction.Stop();
            errors.Should().BeGreaterThan(1);
        }

        [Test]
        public void Should_not_stop_when_action_throws_OperationCanceledException_with_unknown_token()
        {
            var counter = 0;

            Task Action()
            {
                counter++;
                throw new OperationCanceledException(new CancellationToken(true));
            }

            var periodicalAction = new AsyncPeriodicalTimeAction(
                Action,
                errorHandler,
                () => TimeSpan.Zero);

            periodicalAction.Start();

            var spinWait = new SpinWait();
            while (counter < 2)
            {
                spinWait.SpinOnce();
            }

            periodicalAction.Stop();
            errors.Should().BeGreaterThan(1);
        }

        [Test]
        public void Should_stop_when_wait_first_delay()
        {
            var canStop = false;
            var calls = 0;

            TimeSpan Provider()
            {
                canStop = true;
                return TimeSpan.FromSeconds(100500);
            }

            ;

            var periodicalAction = new AsyncPeriodicalTimeAction(
                () =>
                {
                    ++calls;
                    throw new Exception();
                },
                errorHandler,
                Provider,
                true);

            periodicalAction.Start();

            var spinWait = new SpinWait();
            while (!canStop)
            {
                spinWait.SpinOnce();
            }

            periodicalAction.Stop();
            calls.Should().Be(0);
            errors.Should().Be(0);
        }

        [Test]
        public void Should_stop_when_wait_some_delay()
        {
            var canStop = false;
            var calls = 0;

            TimeSpan Provider()
            {
                canStop = true;
                return TimeSpan.FromSeconds(100500);
            }

            ;

            var periodicalAction = new AsyncPeriodicalTimeAction(
                () =>
                {
                    ++calls;
                    return Task.CompletedTask;
                },
                errorHandler,
                Provider);

            periodicalAction.Start();

            var spinWait = new SpinWait();
            while (!canStop)
            {
                spinWait.SpinOnce();
            }

            periodicalAction.Stop();
            errors.Should().Be(0);
            calls.Should().Be(1);
        }

        [Test]
        public void Should_stop_when_action_throws_OperationCanceledException_with_proper_token()
        {
            var canStop = false;

            Task Action(CancellationToken token, TimeSpan remaining)
            {
                canStop = true;

                var actionSpinWait = new SpinWait();
                while (!token.IsCancellationRequested)
                {
                    actionSpinWait.SpinOnce();
                }

                token.ThrowIfCancellationRequested();
                return Task.CompletedTask;
            }

            var periodicalAction = new AsyncPeriodicalTimeAction(
                Action,
                errorHandler,
                () => TimeSpan.Zero);

            periodicalAction.Start();

            var spinWait = new SpinWait();
            while (!canStop)
            {
                spinWait.SpinOnce();
            }

            periodicalAction.Stop();
            errors.Should().Be(0);
        }
    }
}