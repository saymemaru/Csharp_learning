using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多线程_Wait_Pulse
{
    // Monitor
    // 适用于共享资源的场景
    // 必须明确代码执行顺序，避免死锁
    //
    // Wait() 使线程进入等待状态
    // Pulse() 唤醒一个等待状态的线程
    // PulseAll() 唤醒全部等待状态的线程 

    public static class Shared
    {
        public static object lockObject = new();
        public static Queue<int> buffer = new();
        public const int bufferCapacity = 8;

        public static void Print()
        {
            Console.Write("buffer: ");
            foreach (var item in buffer)
            {
                Console.Write($"{item}、");
            }
            Console.WriteLine();
        }
    }


    internal class Producer
    {
        private static Random rand = new();
        const int PRODUCE_TIME = 1000;
        public void Produce()
        {
            Console.WriteLine("开始生产数据");

            for (int i = 0; i < 15; i++)
            {
                lock(Shared.lockObject)
                {
                    if (Shared.buffer.Count == Shared.bufferCapacity)
                    {
                        Console.WriteLine("缓冲区已满，等待消费者信号");
                        Monitor.Wait(Shared.lockObject);
                    }

                    int value = GenerateData();
                    Shared.buffer.Enqueue(value);
                    Console.WriteLine($"生产了 {value}");

                    Shared.Print();

                    //通知消费者
                    Monitor.Pulse(Shared.lockObject);
                }
               
            }

            Console.WriteLine("生产完成");


        }

        private int GenerateData()
        {
            Thread.Sleep(PRODUCE_TIME);
            return rand.Next(0, 100);
        }
    }

    internal class Consumer
    {
        const int CONSUME_TIME = 2000;
        public void Consume()
        {
            Console.WriteLine("开始消费数据");

            for(int i = 0; i < 15; i++) 
            {
                lock (Shared.lockObject)
                {
                    // 队列没有数据
                    if (Shared.buffer.Count == 0)
                    {
                        Console.WriteLine("队列为空，等待数据");
                        Monitor.Wait(Shared.lockObject);
                    }
                        
                }

                Console.WriteLine("正在消费数据");
                Thread.Sleep(CONSUME_TIME);

                lock (Shared.lockObject)
                {
                    int value = Shared.buffer.Dequeue();
                    Console.WriteLine($"消费了 {value}");

                    //不同线程 wait 和 pulse 使用同一锁对象通信
                    Monitor.Pulse(Shared.lockObject);
                }
            }

            Console.WriteLine("消费完成");
        }
    }
}
