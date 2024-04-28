using Aix.StateMachines;
using Aix.StateMachineTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aix.StateMachineTests
{
    public class StateMachineTests
    {

        [Fact]
        public static void Test()
        {
            ITurnstileController controller = new TurnstileController();
            var fsm1 = StateMachineFactory.Instance.CreateStateMachine<TurnstileState, TurnstileEventType>();

            fsm1.Initial(TurnstileState.Locked)
            //若状态在Locked状态收到了一个Coin事件，则迁移到Unlocked状态，并执行Unlock动作。
            .AddTransition(TurnstileState.Locked, TurnstileEventType.Coin, TurnstileState.Unlocked, () => true, t => controller.Unlock())
            //若状态在Locked状态收到了一个Pass事件，则迁移到Locked状态(状态不变)，并执行Alarm动作。
            .AddTransition(TurnstileState.Locked, TurnstileEventType.Pass, TurnstileState.Locked, () => true, t => controller.Alarm())
            //若状态在Unlocked状态收到了一个Pass事件，则迁移到Locked状态，并执行Lock动作。
            .AddTransition(TurnstileState.Unlocked, TurnstileEventType.Pass, TurnstileState.Locked, () => true, t => controller.Lock())
            //若状态在Unlocked状态收到了一个Coin事件，则迁移到Unlocked状态(状态不变)，并执行Thankyou动作。
            .AddTransition(TurnstileState.Unlocked, TurnstileEventType.Coin, TurnstileState.Unlocked, () => true, t => controller.Thankyou());


            //或者这样定义action
            Action<ITransition<TurnstileState, TurnstileEventType>> UnlockAction = t =>
            {
                controller.Unlock();
            };
            var AlarmAction = new Action<ITransition<TurnstileState, TurnstileEventType>>((t) =>
            {
                controller.Alarm();
            });
            var LockAction = new Action<ITransition<TurnstileState, TurnstileEventType>>((t) =>
            {
                controller.Lock();
            });
            var ThankyouAction = new Action<ITransition<TurnstileState, TurnstileEventType>>((t) =>
            {
                controller.Thankyou();
            });

            //或者使用builder模式
            var builder = new StateMachineBuilder<TurnstileState, TurnstileEventType>();
            builder.Initial(TurnstileState.Locked)
            .AddTransition(TurnstileState.Locked, TurnstileEventType.Coin, TurnstileState.Unlocked, () => true, UnlockAction)
            .AddTransition(TurnstileState.Locked, TurnstileEventType.Pass, TurnstileState.Locked, () => true, AlarmAction)
            .AddTransition(TurnstileState.Unlocked, TurnstileEventType.Pass, TurnstileState.Locked, () => true, LockAction)
            .AddTransition(TurnstileState.Unlocked, TurnstileEventType.Coin, TurnstileState.Unlocked, () => true, ThankyouAction);

            var fsm2 = builder.Build();


            IStateMachine<TurnstileState, TurnstileEventType> fsm = fsm2;

            fsm.AddEnterAndLeaveAction(TurnstileState.Locked, t =>
            {
                if (t.Source == t.Target)
                {

                }
                Console.WriteLine($"Enter:{t.Target}");
            }, t =>
            {
                Console.WriteLine($"Leave:{t.Source}");
            });

            fsm.AddEnterAndLeaveAction(TurnstileState.Unlocked, t =>
            {
                Console.WriteLine($"Enter:{t.Target}");
            }, t =>
            {
                Console.WriteLine($"Leave:{t.Source}");
            });

            //test
            fsm.SendEvent(TurnstileEventType.Coin);
            Assert.True(fsm.CurrentState == TurnstileState.Unlocked);

            fsm.SendEvent(TurnstileEventType.Pass);
            Assert.True(fsm.CurrentState == TurnstileState.Locked);

            //fsm.SendEvent(TurnstileEventType.Coin);
            //fsm.SendEvent(TurnstileEventType.Pass);

            //fsm.SendEvent(TurnstileEventType.Pass);
            //fsm.SendEvent(TurnstileEventType.Pass);
            //fsm.SendEvent(TurnstileEventType.Pass);
            //fsm.SendEvent(TurnstileEventType.Coin);
            //fsm.SendEvent(TurnstileEventType.Pass);
        }
    }
}
