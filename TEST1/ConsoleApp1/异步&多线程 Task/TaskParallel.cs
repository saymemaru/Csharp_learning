using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 异步_多线程_Task
{
    // 单线程异步：在一件事完成之前，去做别的事
    // 多线程异步: 多线程实现异步

    // async的作用
        // 指定一个方法为异步方法，其中可以使用await
        // 方法中可以return Task<T> (返回泛型task)
    // await的作用
        // 等待当前任务完成，再继续执行方法

    // 创建任务
        // Task.Run()
        // Task.Factory.StartNew()
        // Task t = new(); t.Run()  类似创建thread

    // 取消任务
        // 1. CancellationTokenSource cts = new(); 创建Token
        // 2. Task.Run(method, cts.token) 传入Token
        // 3. 需要取消任务时调用 cts.Cancel()



    internal class TaskParallel
    {
        public Task PrintA()
        {
            Task.Delay(1000).Wait();
            Console.WriteLine("A");
            return Task.FromResult(0);
        }

        public async Task PrintB(string a)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Delay(1500);
                    Console.WriteLine(a);
                }
            });
        }

        // 题目：
        // 使用 Task.Run 启动一个后台任务，该任务循环 5 次，每次输出当前迭代编号并休眠 1 秒。主线程等待任务完成后输出“任务完成”。
        public void CreateTask()
        {
            Task task = Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine($"任务执行次数： {i}");
                    Task.Delay(1000).Wait(); // 注意：这里用 Wait() 阻塞任务线程，仅用于演示
                }
            });

            task.Wait(); // 等待任务结束
            Console.WriteLine("任务完成");
        }

        // 模拟异步耗时操作
        public static async Task<string> DownloadDataAsync(string filename)
        {
            await Task.Delay(3000); 
            return $"{filename}下载完成";
        }

        //异常处理


        //任务取消
        // 1.创建一个 CancellationTokenSource 对象 cts
        // 2.用 CancellationToken 对象接收cts.Token
        // 3.任务内定期检查 token.IsCancellationRequested
        // 4.执行 cts.Cancel(); 取消循环



        // 任务延续
        // 演示使用ContinueWith当一个任务完成后，接着执行另一个任务
        public void TaskContinueWith()
        {
            Random rnd = new Random();

            Task<int> task1 = Task.Run(() => rnd.Next(1, 100));

            Task task2 = task1.ContinueWith(antecedent =>
            {
                int result = antecedent.Result * 2;
                Console.WriteLine($"原始值：{antecedent.Result}，乘以2后：{result}");
            });

            task2.Wait();
        }


        //演示使用Parallel.For 通过数组索引并行写入数据
        // 因为数据与索引一一对应，所以当前操作线程安全
        public void ParallelFor()
        {
            {
                int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int[] squares = new int[numbers.Length];

                Parallel.For(0, numbers.Length, i =>
                {
                    squares[i] = numbers[i] * numbers[i];
                    Console.WriteLine($"线程 {Task.CurrentId}: {numbers[i]}^2 = {squares[i]}");
                });

                Console.WriteLine("所有计算完成");
            }
        }

        //PLINQ 并行查询

        //线程安全与锁

        //生产者-消费者模式（进阶）
    }

}
