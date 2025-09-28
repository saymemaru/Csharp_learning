using 设计模式___建造者模式;
//建造者模式
//将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示。
//建造者包含所有创建对象的方法,和产品的引用
//指挥者引用建造者的方法，按逻辑顺序执行
Director director = new Director();
Builder builder1 = new ConcreteBuilderA();
Builder builder2 = new ConcreteBuilderB();
director.Construct(builder1);
director.Construct(builder2);
builder1.GetHouse().Show();
builder2.GetHouse().Show();
