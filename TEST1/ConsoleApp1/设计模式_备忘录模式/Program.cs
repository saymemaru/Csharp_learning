using 设计模式_备忘录模式;

//备忘录模式可以记录属性历史，还原信息
//作用：把复杂对象的内部信息对其他对象屏蔽起来
//结构：发起类、备忘录类、管理者类
//缺点：状态信息多时，消耗内存
//发起类有需要保存的信息，可以创建备忘录对象，可以导入备忘录对象恢复信息
//备忘录类有字段存储信息，被创建时传入信息
//管理者类引用被创建的备忘录对象

//创建发起者，修改属性
Originator o = new Originator();
o.State = "Hello";
o.ShowInfo();

//用管理者类保存备忘录
Caretaker c = new Caretaker();
c.Memento = o.CreateMemento();

//修改发起类属性
o.State = "World"; ;
o.ShowInfo();

//将发起者属性恢复为管理者保存的备忘录存储的属性
o.SetMemento(c.Memento);
o.ShowInfo();