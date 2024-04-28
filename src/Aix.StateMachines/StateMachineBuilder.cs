using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines
{
    //下面使用build创建MyStateMachine，学习build模式
    public class StateMachineBuilder<S, E>
    {
        private IStateMachine<S, E> fsm;

        public StateMachineBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.fsm = StateMachineFactory.Instance.CreateStateMachine<S, E>();
        }

        public StateMachineBuilder<S, E> Initial(S state)
        {
            fsm.Initial(state);
            return this;
        }

        public StateMachineBuilder<S, E> AddTransition(S source, E @event, S target, Func<bool> guard = null, Action<ITransition<S, E>> action = null)
        {
            fsm.AddTransition(source, @event, target, guard, action);
            return this;
        }

        public StateMachineBuilder<S, E> AddEnterAndLeaveAction(S source, Action<ITransition<S, E>> enterAction, Action<ITransition<S, E>> leaveAction)
        {
            fsm.AddEnterAndLeaveAction(source, enterAction, leaveAction);
            return this;
        }

        /// <summary>
        /// 最终通过该方法返回对象，重复使用该builder时，要先调用Reset方法即可。
        /// </summary>
        /// <returns></returns>
        public IStateMachine<S, E> Build()
        {
            var result = this.fsm;
            this.Reset(); //这里不调用也行，那么如果要继续使用该Build，就先调用一次Reset();
            return result;
        }
    }
}
