using System;
using System.Collections.Generic;
using System.Text;

namespace Aix.StateMachines
{
    /// <summary>
    /// 事件：现态---条件--->动作->次态   (状态机四要素：现态、条件、动作、次态)
    /// 
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="E"></typeparam>
    public interface ITransition<S, E>
    {
        /// <summary>
        /// 现态,执行事件前的状态
        /// </summary>
        S Source { get; set; }

        /// <summary>
        /// 触发的事件
        /// </summary>
        E Event { get; set; }

        /// <summary>
        /// 次态,执行事件后的状态(如果满足条件)
        /// </summary>
        S Target { get; set; }

        /// <summary>
        /// 条件,是否满足流转条件
        /// </summary>
        Func<bool> Guard { get; set; }

        /// <summary>
        /// 动作,满足流转条件后，执行的行为,动作不是必须的，有时仅仅状态更新
        /// </summary>
        Action<ITransition<S, E>> Action { get; set; }
    }

    
}
