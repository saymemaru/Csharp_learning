using 设计模式__工厂模式;

//输入一个字符串（标识符），输出对应的对象
Product product1 = SimpleFactory.CreateProduct("A");
Product product2 = SimpleFactory.CreateProduct("B");
product1.Use();
product2.Use();


