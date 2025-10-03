using 设计模式___观察者模式;
//观察者模式
//目标：定义对象间的一对多依赖，当一个对象状态发生改变时，所有依赖它的对象都得到通知并被自动更新
//结构：包含订阅者和观察者
//通知者包含事件，订阅观察者的方法
//观察者包含事件触发时的处理方法
//当通知者状态改变时，触发事件，调用观察者的方法   
HealthManager healthManager = new HealthManager();

Player.OnSummoned += healthManager.Player_OnSummoned!;
Player.OnDestroyed += healthManager.Player_OnDestroyed!;
Player.OnDamaged += healthManager.Player_OnDamaged!;

Player Tom = new Player("Tom");
Player Jack = new Player("Jack");
Tom.Damaged(30);
Jack.Damaged(50);
Tom.Damaged(80);


