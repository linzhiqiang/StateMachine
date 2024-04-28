
using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines.Impl
{
    /// <summary>
    ///  FSM，通用有限状态机
    /// </summary>
    /// <typeparam name="S">状态枚举类型</typeparam>
    /// <typeparam name="E">事件枚举类型</typeparam>
    public class StateMachine<S, E> : IStateMachine<S, E>
    {
        #region fields

        /// <summary>
        /// 当前状态
        /// </summary>
        protected S currentState;

        /// <summary>
        /// 迁移表：若状态在Source状态收到了一个Event事件，若满足Guard条件，则迁移到Target状态，并执行Action动作。
        /// </summary>
        protected List<ITransition<S, E>> transitions = new List<ITransition<S, E>>();

        /// <summary>
        /// 进入或离开某个状态时的事件,可以针对某个状态做一些初始化动作。
        /// </summary>
        protected List<EnterAndLeaveAction<S, E>> enterAndLeaveActions = new List<EnterAndLeaveAction<S, E>>();

        #endregion

        #region properies,event

        /// <summary>
        /// 执行完迁移动作后的事件
        /// </summary>
        public event Action<ITransition<S, E>> OnEvent;

        /// <summary>
        /// 执行完离开某个状态后的事件
        /// </summary>
        public event Action<ITransition<S, E>> OnLeaveEvent;

        /// <summary>
        /// 执行完进入某个状态后的事件
        /// </summary>
        public event Action<ITransition<S, E>> OnEnterEvent;


        public S CurrentState { get { return this.currentState; } }

        #endregion

        public StateMachine()
        {

        }

        public IStateMachine<S, E> Initial(S state)
        {
            SetState(state);
            return this;
        }

        protected void SetState(S state)
        {
            currentState = state;
        }

        public IStateMachine<S, E> AddTransition(S source, E @event, S target, Func<bool> guard = null, Action<ITransition<S,E>> action = null)
        {
            transitions.Add(new Transition<S, E>(source, @event, target, guard, action));
            return this;
        }

        public IStateMachine<S, E> AddEnterAndLeaveAction(S source, Action<ITransition<S, E>> enterAction, Action<ITransition<S, E>> leaveAction)
        {
            enterAndLeaveActions.Add(new EnterAndLeaveAction<S, E>(source, enterAction, leaveAction));
            return this;
        }

        public virtual void SendEvent(E @event)
        {
            var transition = transitions.Find(x => x.Source.Equals(currentState) && x.Event.Equals(@event));
            if (transition != null)
            {
                if (transition.Guard != null)
                {
                    if (!transition.Guard.Invoke())
                    {
                        return;
                    }
                }

                var currentStateBak = currentState;

                //leave current state event
                //if (!transition.Source.Equals(transition.Target)) //目标状态和当前状态不一样,这里注释掉，由外部来决定是否执行相应逻辑
                {
                    var leaveAction = enterAndLeaveActions.Find(x => x.State.Equals(currentStateBak));
                    leaveAction?.LeaveAction?.Invoke(transition);

                    OnLeaveEvent?.Invoke(transition);
                }

                //action
                transition.Action?.Invoke(transition);
                OnEvent?.Invoke(transition);

                //set to next state
                SetState(transition.Target);

                //enter new state event
                //if (!transition.Source.Equals(transition.Target))//目标状态和当前状态不一样
                {
                    var enterAction = enterAndLeaveActions.Find(x => x.State.Equals(transition.Target));
                    enterAction?.EnterAction?.Invoke(transition);

                    OnEnterEvent?.Invoke(transition);
                }

            }
        }
    }
}
