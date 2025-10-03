using 设计模式__策略模式;
//目标：定义一系列算法，将每一个算法封装起来，并使它们互不干扰。
//结构：一个策略接口，多个具体策略类，一个上下文类

//策略接口定义方法
//具体策略类实现策略接口的方法
//上下文类持有策略接口的引用，通过调用接口方法，执行具体策略类的方法

//发号施令
string? strategyString = Console.ReadLine();
Context context = new Context(strategyString);
context.ExecuteStrategy();

//例子：
//  cmd输入指令，执行对应的命令
//  帝国时代4，按住shift，为单位添加多个命令【命令队列】
//      当有多个命令时，将他们依次加入队列，依次执行
