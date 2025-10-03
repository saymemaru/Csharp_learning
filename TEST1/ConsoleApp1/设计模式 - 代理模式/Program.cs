using 设计模式_代理模式;

//目标：为其他对象提供一种代理以控制对这个对象的访问
//结构：两个类(具体方法类和代理类)继承同一个方法接口

//方法接口定义方法
//具体方法类实现接口的方法
//代理类有具体方法类的引用，在实现的接口方法中，调用具体方法类的方法
//调用代理类的方法，实际上是调用具体方法类的方法

//借人之手
Target target = new Target(100);
Master master = new Master(target, 10);
Proxy proxy = new Proxy(master);
proxy.EarnMoney();
proxy.StealMoney();
proxy.SpendMoney();
Console.WriteLine($"目标对象的钱：{target.money}");