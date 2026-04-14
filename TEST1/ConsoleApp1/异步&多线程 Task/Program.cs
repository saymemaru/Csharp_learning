using 异步_多线程_Task;

TaskParallel TPL = new TaskParallel();
//Console.WriteLine("K");
/*TPL.PrintB("2");
TPL.PrintB("1");
Console.WriteLine("B");*/

/*CancellationTokenSource cts = new();
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
});*/

// 使用 .Result 获取任务结果（会阻塞当前线程）
// 如果任务已经完成，则直接获得结果
//string result = task.Result;


Console.WriteLine($"主方法开始，线程ID: {Thread.CurrentThread.ManagedThreadId}");

// 启动两个模拟的异步下载任务，但不立即等待它们完成
Task<string> task1 = TaskParallel.DownloadDataAsync("a");
Task<string> task2 = TaskParallel.DownloadDataAsync("b");

// 此时，两个异步操作已经开始，但当前线程并未阻塞，
// 它可以执行其他同步工作（例如打印一些信息）
Console.WriteLine("两个任务已启动，现在可以执行其他工作...");
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"执行其他工作 {i}，线程ID: {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(1000); // 模拟其他小任务（同步操作）
    if (!task1.IsCompleted || !task2.IsCompleted)
    {
        Console.WriteLine("DownloadDataAsync执行中");
    }
    else
    {
        Console.WriteLine("DownloadDataAsync已完成");
    }
}

// 现在我们需要任务的结果，所以等待它们完成
// 注意：这里使用 await 会异步等待，而不会阻塞线程
string[] results = await Task.WhenAll(task1, task2);

Console.WriteLine($"所有任务完成，结果: {string.Join(", ", results)}，线程ID: {Thread.CurrentThread.ManagedThreadId}");



