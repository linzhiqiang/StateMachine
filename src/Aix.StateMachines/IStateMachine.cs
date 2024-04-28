using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines
{

    public interface IStateMachine<S, E>
    {
        S CurrentState { get; }
        IStateMachine<S, E> Initial(S state);

        IStateMachine<S, E> AddTransition(S source, E @event, S target, Func<bool> guard = null, Action<ITransition<S, E>> action = null);

        IStateMachine<S, E> AddEnterAndLeaveAction(S source, Action<ITransition<S, E>> enterAction, Action<ITransition<S, E>> leaveAction);
        void SendEvent(E @event);
    }

    
}
