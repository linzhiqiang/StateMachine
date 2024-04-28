
using Aix.StateMachines.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines
{
    public class StateMachineFactory
    {
        public static StateMachineFactory Instance = new StateMachineFactory();

        public IStateMachine<S, E> CreateStateMachine<S, E>()
        {
            return new StateMachine<S, E>();
        }
    }
}
