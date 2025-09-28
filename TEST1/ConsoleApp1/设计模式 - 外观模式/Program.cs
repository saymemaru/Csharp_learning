using 设计模式___外观模式;

Facade facade = new Facade();
facade.MethodA();
facade.MethodB();

//例子
//数据访问层、业务逻辑层、表示层,两两之间建立外观
//将复杂的子系统调用封装在外观类中,对外提供简单的接口