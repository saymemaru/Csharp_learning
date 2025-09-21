namespace 静态
{
    //泛型静态类
    public static class Static_<T>
    {
        //测试静态类和非静态类
        //静态类不能实例化对象，所有成员必须是静态的
        //静态类通常用于存放工具方法或全局状态
        //非静态类可以实例化对象，成员可以是静态的或实例的

        public static string field;
        public static void PrintField()
        {
            Console.WriteLine(field + ": " + typeof(T).Name);
        }
    }

    public class NonStatic_
    {
        //静态字段
        public static int 静态字段 = 0;
        //实例字段
        public int 实例字段 = 0;
        //静态方法
        public static void 静态方法()
        {
            静态字段++;
            //实例字段++; //错误，静态方法不能访问实例字段
            NonStatic_ obj = new NonStatic_();
            obj.实例字段++;
            Console.WriteLine($"静态方法被调用了{静态字段}次,实例字段值为{obj.实例字段}");
        }
        //实例方法
        public void 实例方法()
        {
            静态字段++;
            实例字段++;
            Console.WriteLine($"实例方法被调用了{静态字段}次,实例字段值为{实例字段}");
        }
    }

    //嵌套泛型类型的静态构造函数
    public class Outer<T>
    {
        public class Inner<U, V>
        {
            //静态构造函数
            //静态构造函数在第一次访问类的任何静态成员 或 创建类的第一个实例之前运行
            static Inner()
            {
                Console.WriteLine("Outer<{0}>.Inner<{1},{2}>",
                                   typeof(T).Name,
                                   typeof(U).Name,
                                   typeof(V).Name);
            }
            //实例构造函数
            //实例构造函数在创建类的每个实例时运行
            Inner()
            {

            }
            public static void DummyMethod() { }
        }
    }


}

