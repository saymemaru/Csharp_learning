using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多线程
{
    // https://zhuanlan.zhihu.com/p/1953053041126245114

    /*分时操作：操作系统将时间分为多个片段，尽可能均匀分配给正在执行的“程序”（线程），
        获得时间段的程序执行，其他则等待它，cpu不断切换执行这些程序

    进程：程序边界，包含程序所需资源的内存区域，是操作系统进行资源分配的单元
    线程：进程中的一个最小执行单元，指向方法，是CPU分配时间片的单位
        +可以并发执行
        +同一个程序的线程共享堆内存（类的成员变量）*/

    // 线程不安全：多个线程访问同一个资源时，导致数据结果不一致/不可预测
    // 同步：协调多个线程的执行顺序，同时只允许一个线程访问同一资源
    // 原子操作：系统底层的操作，只会执行或不执行，不会执行到一半被中断（请使用.net封装的api）
    internal class ThreadExample
    {
        // 无参数
        Thread test1 = new(ThreadMethod1);
        // 匿名函数传参
        Thread test3 = new((obj) =>
        {
            Console.WriteLine(obj);
        })
        {IsBackground = true, Priority = ThreadPriority.Normal}; // 

        private static void ThreadMethod1()
        {
        }

        public static void Main(Thread thread)
        {
            //开始线程
            thread.Start();
            // 线程睡眠（阻塞）
            Thread.Sleep(1000);
            // 等待线程执行完，再继续执行
            thread.Join();
            Console.WriteLine("over");
        }
 

        

        
      
    }
}
