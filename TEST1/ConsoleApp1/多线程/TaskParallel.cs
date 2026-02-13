using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多线程
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


    }
}
