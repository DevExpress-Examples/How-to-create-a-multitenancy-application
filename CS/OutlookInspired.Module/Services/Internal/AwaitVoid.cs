using System.Collections.Concurrent;

namespace OutlookInspired.Module.Services.Internal{
    public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs e);
    internal static class AwaitVoidService{
        [Obsolete]
        public static void Await(this object any, Func<Task> invoker) {
            var originalContext = SynchronizationContext.Current;
            try {
                var context = new SingleThreadedSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(context);
                var task = invoker.Invoke();
                task.ContinueWith(_ => context.Queue.CompleteAdding());
                while (context.Queue.TryTake(out var work, Timeout.Infinite))
                    work.d.Invoke(work.state);
                task.GetAwaiter().GetResult();
            }
            finally {
                SynchronizationContext.SetSynchronizationContext(originalContext);
            }
        }

    }

    internal sealed class SingleThreadedSynchronizationContext : SynchronizationContext {
        public BlockingCollection<(SendOrPostCallback d, object state)> Queue{ get; } = new();

        public override void Post(SendOrPostCallback d, object state){
            if (!Queue.IsAddingCompleted) {
                Queue.Add((d, state));
            }
        }
    }
}