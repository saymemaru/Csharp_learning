using 静态;

//静态方法不用实例化对象即可调用
//静态成员只要被分配内存就存在，直到程序结束
//初始化优先初始化类的静态成员，然后才是实例成员
NonStatic_.静态方法();
NonStatic_ obj1 = new NonStatic_();
obj1.实例方法();

//泛型静态类
//每个不同类型的T都会有一个独立的静态字段
Static_<int>.field = "First";
Static_<string>.field = "Second";
Static_<DateTime>.field = "Third";
Static_<int>.PrintField();
Static_<string>.PrintField();
Static_<DateTime>.PrintField();

//同样的规则也适用于静态初始化程序（static initializer）和静态构造函数（static constructor）
//嵌套泛型类型的静态构造函数
Outer<int>.Inner<string, DateTime>.DummyMethod();
Outer<object>.Inner<string, object>.DummyMethod();
Outer<object>.Inner<object, string>.DummyMethod();
Outer<string>.Inner<string, object>.DummyMethod();
//封闭类型的静态构造函数只在第一次访问类的任何静态成员执行一次。所以不会产生第6行输出
Outer<string>.Inner<int, int>.DummyMethod();
Outer<string>.Inner<int, int>.DummyMethod();