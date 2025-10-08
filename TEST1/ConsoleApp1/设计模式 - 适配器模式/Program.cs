using 设计模式___适配器模式;

//将一个类的接口转换成另一个接口
//常用于修补未预留接口的地方
//
//适配器类继承目标类
//适配器类中包含一个被适配类对象, 并且有方法调用被适配类的对应方法
//
//目标类引用指向适配器对象
//这样目标类对象就能调用被适配类的方法
英语壬 translator = new 翻译壬();
英语壬 englisher = new 英语壬();

translator.SayHello();
englisher.SayHello();

//.net DataAdapter类适配不同数据库
