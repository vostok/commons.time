using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Commons.Time
{
    /// <summary>
    /// Periodical action executed on thread pool
    /// </summary>
    [PublicAPI]
    internal class AsyncPeriodicalTimeAction
    {
        private readonly Func<CancellationToken, TimeSpan, Task> action;
        [NotNull]
        private readonly Action<Exception> errorHandler;
        [NotNull]
        private readonly Func<TimeSpan> period;
        private readonly bool delayFirstIteration;
        private readonly object syncLock = new object();

        private volatile Task workerTask;
        private volatile CancellationTokenSource cancellationSource;

        public AsyncPeriodicalTimeAction(
            [NotNull] Func<Task> action,
            [NotNull] Action<Exception> errorHandler,
            [NotNull] Func<TimeSpan> period,
            bool delayFirstIteration = false)
            : this((ct, time) => action(), errorHandler, period, delayFirstIteration)
        {
        }

        public AsyncPeriodicalTimeAction(
            [NotNull] Func<CancellationToken, TimeSpan, Task> action,
            [NotNull] Action<Exception> errorHandler,
            [NotNull] Func<TimeSpan> period,
            bool delayFirstIteration = false)
        {
            this.action = action;
            this.errorHandler = errorHandler;
            this.period = period;
            this.delayFirstIteration = delayFirstIteration;
        }

        public bool IsRunning
        {
            get
            {
                lock (syncLock)
                {
                    return workerTask != null;
                }
            }
        }

        public void Start()
        {
            lock (syncLock)
            {
                if (workerTask != null)
                    return;

                cancellationSource = new CancellationTokenSource();
                workerTask = Task.Run(() => WorkerRouting(cancellationSource.Token));
            }
        }

        public void Stop()
        {
            lock (syncLock)
            {
                if (workerTask == null)
                    return;

                cancellationSource.Cancel();
                workerTask.GetAwaiter().GetResult();

                cancellationSource.Dispose();
                workerTask.Dispose();

                cancellationSource = null;
                workerTask = null;
            }
        }

        private static async Task DelaySafe(TimeSpan delay, CancellationToken token)
        {
            try
            {
                await Task.Delay(delay, token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // OK
            }
        }

        private async Task WorkerRouting(CancellationToken token)
        {
            if (delayFirstIteration)
                await DelaySafe(period(), token);

            while (!token.IsCancellationRequested)
            {
                var budget = TimeBudget.StartNew(period(), TimeSpan.FromMilliseconds(1));

                try
                {
                    await action(token, budget.Remaining);
                }
                catch (OperationCanceledException e) when (e.CancellationToken == token && token.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception error)
                {
                    errorHandler(error);
                }

                var remainingBudget = budget.Remaining;
                await DelaySafe(remainingBudget, token);
            }
        }
    }
}