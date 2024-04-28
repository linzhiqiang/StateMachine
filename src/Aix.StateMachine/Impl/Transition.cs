using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines.Impl
{
    public class Transition<S, E> : ITransition<S, E>
    {
        public S Source { get; set; }

        public E Event { get; set; }

        public S Target { get; set; }

        public Func<bool> Guard { get; set; }

        public Action<ITransition<S, E>> Action { get; set; }

        public Transition()
        {
        }
        public Transition(S source, E @event, S target, Func<bool> guard = null, Action<ITransition<S, E>> action = null)
        {
            Source = source;
            Event = @event;
            Target = target;
            Guard = guard;
            Action = action;
        }

    }
}
