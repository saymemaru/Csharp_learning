using 设计模式_原型模式;

//原型模式
//用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象
//根据一个对象，创建出和原对象类似的新对象
//优点:不用重新初始化一个对象，节省资源

Console.WriteLine("------浅拷贝------");
//1.浅拷贝
ConcretePrototypeA pA1 = new ConcretePrototypeA("I am the original");
ConcretePrototypeA pB2 = (ConcretePrototypeA)pA1.Clone();

//.net ICloneable接口的浅拷贝
//缺点：对于引用类型的成员，拷贝的是引用地址
PersonA p1 = new PersonA("123456789");
p1.SetHomeAddress("JP","Tokey");

PersonA p2 = (PersonA)p1.Clone();
p2.SetPhoneNum("987654321");//修改值类型
p2.SetHomeAddress("CN", "Beijing");//修改引用类型

PersonA p3 = (PersonA)p1.Clone();
p3.SetPhoneNum("555555555");
p3.SetHomeAddress("US", "New York");

p1.ShowInfo();
p2.ShowInfo();
p3.ShowInfo();

Console.WriteLine("------深拷贝------");
//2.深拷贝
//让引用对象也实现ICloneable接口
//重写Clone方法,在私有构造函数中调用引用对象的Clone方法

//注意:使用前需要考虑引用类型成员嵌套引用类型的情况,事先确认复制的层级
PersonB p4 = new PersonB("123456789");
p4.SetHomeAddress("JP", "Tokey");

PersonB p5 = (PersonB)p4.Clone();
p5.SetPhoneNum("987654321");//修改值类型
p5.SetHomeAddress("CN", "Beijing");//修改引用类型

PersonB p6 = (PersonB)p4.Clone();
p6.SetPhoneNum("555555555");
p6.SetHomeAddress("US", "New York");

p4.ShowInfo();
p5.ShowInfo();
p6.ShowInfo();

//简化版
/*Cat cat1 = new Cat();
cat1.SetHomeAddress("JP", "Tokey");
Cat cat2 = (Cat)cat1.Clone();
cat2.SetHomeAddress("CN", "Beijing");
Cat cat3 = (Cat)cat1.Clone();
cat3.SetHomeAddress("US", "New York");

cat1.ShowInfo();
cat2.ShowInfo();
cat3.ShowInfo();*/