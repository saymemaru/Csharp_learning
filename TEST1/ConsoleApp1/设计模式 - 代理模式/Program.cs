using 设计模式_代理模式;

//两个类(具体方法类和代理类)继承同一个方法接口
//代理类有具体方法类的引用，通过实现接口方法，调用具体方法类的方法

//实现，调用代理类的方法，实际上是调用具体方法类的方法

Target target = new Target(100);
Master master = new Master(target, 10);
Proxy proxy = new Proxy(master);
proxy.EarnMoney();
proxy.StealMoney();
proxy.SpendMoney();
Console.WriteLine($"目标对象的钱：{target.money}");