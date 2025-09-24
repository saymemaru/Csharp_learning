using 设计模式__装饰模式;
//在不修改原有类文件和使用继承的情况下，添加新的功能

//ConcreteComponent 和 Decorator 继承同一个 Component
//通过将上一层 Component 的引用传递给另一个 Decorator
//以在不改变原 Component 结构的情况下，给它添加一些额外功能
ConcreteComponent cc = new ConcreteComponent();
ConcreteDecoratorA decoratorA = new ConcreteDecoratorA();
ConcreteDecoratorB decoratorB = new ConcreteDecoratorB();

//装饰过程就是在上一个组件的基础上添加新的功能
//添加的顺序可以任意组合
//但是要考虑到装饰之间的逻辑关系
decoratorA.Decorate(cc);
decoratorB.Decorate(decoratorA);
decoratorB.Decorate(decoratorB);

decoratorB.Operation();

//例子：
//  组件：枪械类：ak47、m4a1、g36c
//  装饰器：配件类：红点、消音器、激光瞄准
//  通过不同的配件组合，来实现不同的枪械功能/性能
//  
//  组件：实体类：玩家、僵尸、骷髅
//  装饰器：装备类：头盔、胸甲、护腿、靴子
//  【当前装饰模式没有考虑需要随意移除装饰的情况】
//  【更多请参考 《组合模式》：用一个集合装下所有"装饰"的引用】
//  
//  组件：文本类：TextView、ImageView
//  装饰器：配件类：ScrollBarDecorator、BorderDecorator
//  通过不同的配件组合，来实现不同的文本显示效果
