using 设计模式___模板方法;


//模板方法模式
//目标：使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤
//结构：主体为一个抽象类，包含一个模板方法，和若干个基本方法，具体类实现所有基本方法
//模板方法由基本方法组成，基本方法由具体类实现

//同一张试卷,不同的答案
AbstractClass abstractClass;
abstractClass = new ConcreteClassA(); 
abstractClass.TemplateMethod();
abstractClass = new ConcreteClassB();
abstractClass.TemplateMethod();

