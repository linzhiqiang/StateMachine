using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines
{
    /// <summary>
    /// State的进入和离开事件实体
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="E"></typeparam>
    public class EnterAndLeaveAction<S, E>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public S State { get; private set; }

        /// <summary>
        /// 离开当该状态时执行的行为
        /// </summary>
        public Action<ITransition<S, E>> EnterAction { get; private set; }

        /// <summary>
        /// 进入该状态时执行的行为
        /// </summary>
        public Action<ITransition<S, E>> LeaveAction { get; private set; }

        public EnterAndLeaveAction(S s, Action<ITransition<S, E>> enter, Action<ITransition<S, E>> leave)
        {
            this.State = s;
            this.EnterAction = enter;
            this.LeaveAction = leave;

        }
    }
}
