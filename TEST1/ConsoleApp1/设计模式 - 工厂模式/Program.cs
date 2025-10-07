using System.Configuration;
using System.Reflection;
using 设计模式_工厂模式;

//一、简单工厂
//目标：输出对应具体产品类的对象
//结构：工厂类的静态方法根据传入的参数返回不同的具体产品类的实例

//优点：客户端不需要知道具体产品类的类名
//缺点：增加方法要修改简单工厂类

Product productA1 = SimpleFactory.CreateProduct("A");
Product productB1 = SimpleFactory.CreateProduct("B");
productA1.Use();
productB1.Use();

//二、方法工厂
//结构：包含一个工厂接口，以及多个实现该接口的具体工厂类
//优点：克服了简单工厂的缺点
//缺点：每增加一个具体产品类，需增加一个具体工厂类

IFactory productFactoryA1 = new ProductAFactory();
IFactory productFactoryB1 = new ProductBFactory();
Product productA2 = productFactoryA1.CreateProduct();
Product productB2 = productFactoryB1.CreateProduct();
productA2.Use();
productB2.Use();

//三、抽象方法工厂
//结构：包含一个抽象工厂接口，多个实现该接口的具体工厂类, 多个抽象产品接口, 以及具体产品类
//优点：增加产品族时，只需增加具体工厂类和具体产品类, 客户端只需要知道抽象工厂和抽象产品
//缺点：增加产品种类时，需要修改抽象工厂接口及其所有具体工厂类(有多少具体工厂就要修改多少处)
//      创建工厂时, 客户端仍然需要知道具体工厂类
IAbstractFactory dogFactory = new DogFactory();
IAbstractFactory catFactory = new CatFactory();
IAnimal cat = catFactory.CreateAnimal();
IAnimal dog = dogFactory.CreateAnimal();
IFood dogFood = dogFactory.CreateFood();
dog.Use();
cat.Use();
dogFood.Eat();
Console.WriteLine();

//四、反射+配置文件简单工厂
//彻底隔离客户端和具体产品类
IFood food1 = Factory.CreateFood();
IAnimal animal1 = Factory.CreateAnimal();
food1.Eat();
animal1.Use();



