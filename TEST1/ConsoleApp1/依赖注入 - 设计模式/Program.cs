// Dependency Injection, DI
// https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
//一个类不应该自己创建它所依赖的对象，而是应该由外部容器（或调用者）“注入”给它
//
//依赖（Dependency）： 如果一个类 A 需要调用类 B 的方法来实现功能，那么类 B 就是类 A 的依赖。
//例如：Person 类 可能需要 Glass 类 来看清楚东西，那么 Glass 类就是 Person 类的依赖。
//人需要眼镜来帮助看清楚东西。

//注入（Injection）： 依赖对象类 B 通过某种方式（如构造函数、属性、方法）传递给依赖它的类 A，
//例如：在 Person 类的构造函数中传入一个 Glass 对象，而非让 Person 类自带一个 Glass 对象。
//人去房间里找到眼镜，而不是自己变异一个眼镜在脸上。
//同时根据人的需求，可以选择不同的眼镜（近视眼镜、远视眼镜、太阳镜等）（IGlass）。
//
//你已经在Fr_Robot中使用过依赖注入了，
//你将 ICommand 的各种实现在构造函数中注册到了 TCPServe r的 _commandDic 中，
//如果能将这个注册过程交给外部容器来完成就更好了。

using 设计模式_依赖注入;

Person personA = new Person(new SunGlass());
Person personB = new Person(new ColorGlass());
personA.Watch();
personB.Watch();