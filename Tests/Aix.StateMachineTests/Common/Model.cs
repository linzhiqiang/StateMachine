using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aix.StateMachineTests.Common
{
    /// <summary>
    /// 地铁旋转门类型
    /// </summary>
    public enum TurnstileState
    {
        Locked,
        Unlocked
    }

    /// <summary>
    /// 地铁旋事件
    /// </summary>
    public enum TurnstileEventType
    {
        Coin,
        Pass
    }

    /// <summary>
    /// 有4个指令操作
    /// </summary>
    public interface ITurnstileController
    {
        /// <summary>
        /// 关闭
        /// </summary>
        void Lock();


        /// <summary>
        /// 打开
        /// </summary>
        void Unlock();

        /// <summary>
        /// 仅仅提示 谢谢
        /// </summary>
        void Thankyou();

        /// <summary>
        /// 发出一声警告
        /// </summary>
        void Alarm();
    }

    public class TurnstileController : ITurnstileController
    {
        public void Lock()
        {
            Console.WriteLine("Lock");
        }

        public void Unlock()
        {
            Console.WriteLine("Unlock");
        }

        public void Thankyou()
        {
            Console.WriteLine("Thankyou");
        }

        public void Alarm()
        {
            Console.WriteLine("Alarm");
        }

    }
}
