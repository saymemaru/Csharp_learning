namespace 静态
{
    public static class Static_
    {
        //测试静态类和非静态类
        //静态类不能实例化对象，所有成员必须是静态的
        //静态类通常用于存放工具方法或全局状态
        //非静态类可以实例化对象，成员可以是静态的或实例的
        public static void Test()
        {
            NonStatic_.静态方法();
            NonStatic_ obj1 = new NonStatic_();
            obj1.实例方法();
            NonStatic_ obj2 = new NonStatic_();
            obj2.实例方法();
            NonStatic_.静态方法();
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


}

