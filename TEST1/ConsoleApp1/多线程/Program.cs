// https://www.bilibili.com/video/BV1PQrwBDEZt?spm_id_from=333.788.player.switch&vd_source=c737187c622865f81e4ddd178dab24d7&p=176
using System.Threading.Tasks;
using System.Timers;
using 多线程;
using static 多线程.Damagement;

# region Thread属性
//获取当前的工作线程线程
Thread mainThread = Thread.CurrentThread;
mainThread.Name = "Main Thread";
ThreadPriority priority = mainThread.Priority; // 给线程分配时间的优先级
ThreadState state = mainThread.ThreadState;
// 线程属性
Console.WriteLine($"当前工作线程：{mainThread.Name} 优先级：{priority.ToString()}");
Console.WriteLine($"IsBackground：{mainThread.IsBackground} IsAlive：{mainThread.IsAlive}");
Console.WriteLine($"state：{state.ToString()}");
Console.WriteLine($"threadId：{mainThread.ManagedThreadId}");

# endregion

//创建线程，给线程传入委托，启动线程
# region 参数化线程委托

int healCount = 20;
ParameterizedThreadStart heal = new((obj) =>
{ 
    int? count = (int?)obj;
    Example.Heal(count); 
});
Thread threadHeal = new(heal);

# endregion 

# region 给线程传入带有回调函数的实例方法委托，

/*当一个方法过于复杂、需要大量局部变量或拆分为多个子方法时，
可以把它提取到一个独立的类中，原方法变成该类的一个实例方法*/
Damagement damagement = new() { Count = 20 };
// 回调函数，线程完成后执行的方法
// 用于在线程之间通讯，传递返回值
Action<int> callback = (int sum) =>
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n返回 threadDamage 的值：{sum}");
};
ThreadStart damage = new(() =>
{
    //传入回调函数的实例委托
    damagement.Damage(callback);
    /*damagement.Damage((int sum) => 
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n返回 threadDamage 的值：{sum}");
    });*/
});
Thread threadDamage = new(damage) 
{ IsBackground = true, Priority = ThreadPriority.Normal }; //设置是否为后台线程、优先级

# endregion 

threadHeal.Start(healCount);
threadDamage.Start();

// sleep
// 让当前线程处于闲置
Thread.Sleep(1000);

// interrupt
// 中断线程，在中断处的语句抛出异常
//threadHeal.Interrupt();

// join
//主线程等待thread1/2完成再继续执行
threadHeal.Join();
threadDamage.Join();

Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine($"线程共享生命值：{Shared.Health}");
Console.WriteLine($"\n{mainThread.Name} 已完成");


