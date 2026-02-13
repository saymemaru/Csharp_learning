
using 多线程;

TaskParallel TPL = new TaskParallel();
//Console.WriteLine("K");
/*TPL.PrintB("2");
TPL.PrintB("1");
Console.WriteLine("B");*/

CancellationTokenSource cts = new();
Task _ = Task.Run(async () =>
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(999);
        Console.WriteLine(i);
    }
}
, cts.Token);
Thread.Sleep(3000);
cts.Cancel();


Task<string> task = Task.Run(() =>
{
    Task.Delay(2000).Wait(); // 模拟耗时操作
    return "任务完成！";
});

// 使用 .Result 获取任务结果（会阻塞当前线程）
// 如果任务已经完成，则直接获得结果
string result = task.Result;
