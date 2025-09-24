using 设计模式__策略模式;

//输入一个字符串（标识符），获取对应的策略对象【简单工厂】
//执行对应的策略（不由策略对象执行，而由context代为执行）

Context context;
while(true)
{
    Console.WriteLine("【策略模式】输入想要执行的命令：");
    context = new Context(Console.ReadLine());
    context.ExecuteStrategy();
}

//例子：
//  cmd输入指令，执行对应的命令
//  帝国时代4，按住shift，为单位添加多个命令【命令队列】
//      当有多个命令时，将他们依次加入队列，依次执行
