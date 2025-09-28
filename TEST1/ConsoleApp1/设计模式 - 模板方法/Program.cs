using 设计模式___模板方法;


//抽象类的模板方法决定了基本方法的执行逻辑,而具体类实现了基本方法
AbstractClass abstractClass;

abstractClass = new ConcreteClassA(); 
abstractClass.TemplateMethod();

abstractClass = new ConcreteClassB();
abstractClass.TemplateMethod();

//同一张试卷,不同的答案