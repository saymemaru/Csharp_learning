using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多线程_Signaling
{
    // 适合线程间仅传递信号的场景
    // 手动门
    // ManualResetEvent.Set(); 调用后保持状态不变（即使未来调用WaitOne）
    // ManualResetEvent.Reset(); 手动重置状态（状态取决于初始值）
    // 自动门
    // AutoResetEvent.Set(); 调用后自动重置状态
    //
    // ResetEvent.WaitOne() 线程开始等待恢复信号

    internal class Shared
    {
        public static int[] Data { get; set; } //存储线程生成的数据
        /*public static int BetchCount { get; set; }
        public static int BetchSize { get; set; }*/

        //手动重置事件对象
        //Shared.ConsumerEvent.Set(); 调用后保持状态不变
        //Shared.ConsumerEvent.Reset(); 手动重置状态
        public static ManualResetEvent ConsumerEvent { get; set; }
        public static ManualResetEvent ProducerEvent { get; set; }

        //自动重置事件
        // Shared.AutoResetEvent.Set(); 调用后自动重置状态
        public static AutoResetEvent AutoResetEvent { get; set; }
        public Shared() 
        {
            Data = new int[15];

            // false等待，true启动
            ConsumerEvent = new ManualResetEvent(false);
            ProducerEvent = new ManualResetEvent(false);
        }
    }
    internal class Producer
    {
        private static Random rand= new();
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} 启动");

            while(true)
            {
                //生产数据
                Console.WriteLine($"{Thread.CurrentThread.Name} 开始生产");
                for (int j = 0; j < Shared.Data.Length; j++)
                {
                    Shared.Data[j] = rand.Next(0, 100);
                    Thread.Sleep(150);
                    /*for (int i = 0; i < Shared.BetchSize; i++)
                    {
                        
                    }*/
                }
 
                Console.WriteLine($"{Thread.CurrentThread.Name} 生产完成");

                // 启动consumer线程
                Shared.ConsumerEvent.Set();
                Shared.ConsumerEvent.Reset();

                // 等待consumer线程发送信号
                //Console.WriteLine($"{Thread.CurrentThread.Name} 正在等待信号");
                Shared.ProducerEvent.WaitOne();
                
            }
        }
    }
    internal class Consumer
    {
        private static Random rand = new();
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} 启动");

            // 等待信号开始执行  
            while(true)
            {
                // 等待producer线程发送信号
                //Console.WriteLine($"{Thread.CurrentThread.Name} 正在等待信号");
                Shared.ConsumerEvent.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} 开始消费");

                for (int i = 0; i < Shared.Data.Length; i++)
                {
                    Console.Write($"{Shared.Data[i]},");
                }
                Console.WriteLine();
                int time = rand.Next(1000, 7000);
                Thread.Sleep(time);
                Console.WriteLine($"{Thread.CurrentThread.Name} 消费完成，耗时 {time} ms\n");

                // 启动producer线程
                Shared.ProducerEvent.Set();
                Shared.ProducerEvent.Reset();
            }
            
        }
    }
}
