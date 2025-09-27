using 设计模式_代理模式;

Target target = new Target(100);
Master master = new Master(target, 10);
Proxy proxy = new Proxy(master);
proxy.EarnMoney();
proxy.StealMoney();
proxy.SpendMoney();
Console.WriteLine($"目标对象的钱：{target.money}");