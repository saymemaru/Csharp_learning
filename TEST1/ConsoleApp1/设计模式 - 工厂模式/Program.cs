using 设计模式_工厂模式;

//一、简单工厂
//创建一个简单工厂类，输出对应具体产品类的对象
//优点：客户端不需要知道具体产品类的类名
//缺点：增加方法要修改简单工厂类
Product productA1 = SimpleFactory.CreateProduct("A");
Product productB1 = SimpleFactory.CreateProduct("B");
productA1.Use();
productB1.Use();

//二、方法工厂
//创建一个工厂接口，以及多个实现该接口的具体工厂类
//优点：克服了简单工厂的缺点
//缺点：每增加一个具体产品类，需增加一个具体工厂类
IFactory productFactoryA1 = new ProductAFactory();
IFactory productFactoryB1 = new ProductBFactory();
Product productA2 = productFactoryA1.CreateProduct();
Product productB2 = productFactoryB1.CreateProduct();
productA2.Use();
productB2.Use();
