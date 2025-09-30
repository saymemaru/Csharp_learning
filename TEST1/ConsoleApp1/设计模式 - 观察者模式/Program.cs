using 设计模式___观察者模式;

HealthManager healthManager = new HealthManager();

Player.OnSummoned += healthManager.Player_OnSummoned!;
Player.OnDestroyed += healthManager.Player_OnDestroyed!;
Player.OnDamaged += healthManager.Player_OnDamaged!;

Player Tom = new Player("Tom");
Tom.Damaged(30);
Tom.Damaged(80);


