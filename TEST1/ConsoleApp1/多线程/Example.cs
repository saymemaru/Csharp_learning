using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static 多线程.Damagement;

namespace 多线程
{
    // https://zhuanlan.zhihu.com/p/1953053041126245114

    /*分时操作：操作系统将时间分为多个片段，尽可能均匀分配给正在执行的“程序”（线程），
        获得时间段的程序执行，其他则等待它，cpu不断切换执行这些程序

    进程：程序边界，包含程序所需资源的内存区域，是操作系统进行资源分配的单元
    线程：进程中的一个最小执行单元，指向方法，是CPU分配时间片的单位
        +可以并发执行
        +同一个程序的线程共享堆内存（类的成员变量）
        +有独立的栈内存
    */

    // 线程不安全/竞态（race condition）：多个线程访问同一个资源时，导致数据结果不一致/不可预测
    // 死锁（deadlock）：多个线程互相等待对方释放资源（锁对象）
    // 饥饿（starvation）：一个线程优先级过高，长期占有资源

    // 线程同步：当访问共享资源时，协调多个线程的执行顺序，同时只允许一个线程访问同一资源
    // Monitor 监视器
    // Locks 锁（Monitor的语法糖）
    // Mutex 互斥体
    // Semaphores 信号量
    // 原子操作：系统底层的操作，只会执行或不执行，不会执行到一半被中断（请使用.net封装的api）

    // 前/后台线程：前台线程会阻止应用关闭，直到前台线程结束
    /*  上下文切换（context swtich）：从一个进程切换到另一个进程的过程
             时间片耗尽：当前运行的线程用完了分配给它的CPU时间片。
             I/O操作：线程执行I/O操作，必须等待I/O操作完成。
             高优先级线程到来：当前运行的线程被高优先级的线程抢占。
             多处理器调度：在多处理器系统中，为了负载均衡，可能会将线程从一个处理器迁移到另一个。
             同步和锁：线程在等待锁释放时可能被挂起。*/

    /* ThreadState线程状态
            unstart
            running
            waitsleepjoin
            stop
    */


    internal class Example
    {
        #region 创建线程
        // 无参数
        Thread t1 = new(ThreadMethod1);
        // 匿名函数传参
        Thread t2 = new((obj) =>
        {
            Console.WriteLine(obj);
        })
        {IsBackground = true, Priority = ThreadPriority.Normal}; //前后台线程，优先级
        public static void ThreadMethod1()
        {
        }
        #endregion

        public static void Heal(int? count)
        {
            try
            {
                Console.WriteLine("开始治疗");

                for (int i = 0; i < count; i++)
                {
                    // 线程访问共享资源
                    lock(Shared.lockObject)
                    {
                        Shared.Health++;
                    }
                   
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Heal{i},");
                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex);
            }    
        }
    }

    //线程中调用的类
    internal class Damagement
    {
        // 属性
        public int Count { get; set; }
        // 方法，回调函数
        public void Damage(Action<int> callback)
        {
            int sum = 0;
            try
            {
                Console.WriteLine("开始伤害"); 
                for (int i = 0; i < Count; i++)
                {
                    // 线程访问共享资源
                    lock (Shared.lockObject)
                    {
                        Shared.Health--;
                    }

                    sum += i;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Damage{i},");
                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException ex)
            {
            }
            finally
            {
                callback?.Invoke(sum);
            }
        }
    }

    // 假设为多线程之间的共享资源
    internal class Shared
    {
        public static int Health { get; set; } = 0;


        
        // 锁对象，同一共享资源使用同一个锁对象锁定
        public static readonly Object lockObject = new();

        // Monitor
        /*Monitor.Enter(lockObject);
            ...
        Monitor.Exit(lockObject);
        ----- 等价于 -----
        // lock语法糖
        lock(lockObject){...}*/


    }
}
