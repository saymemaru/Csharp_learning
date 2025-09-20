//委托
// 引用方法，将方法作为参数传递，或者将方法赋值给变量
//1.声明委托类型 
//2.创建一个方法包含了要执行的代码；
//3.创建一个委托实例（传入方法） 
//4.调用（invoke）委托实例。
using 委托;
using static 委托.MyDelegate;

Person 小王 = new Person("小王");
Person 小李 = new Person("小李");
StringProcessor 小王说 = new StringProcessor(小王.Say);
StringProcessor 小李说 = new StringProcessor(小李.Say);
StringProcessor 背景 = new StringProcessor(Background.Note);
小王说("啊我死了!");
小李说("你怎么死了");//隐式调用
背景?.Invoke("死人笑声");//显式调用invoke

//委托实例可以合并到一起（调用时按合并顺序）
背景 += 小王说; 
背景 += 小李说;
背景("给您拜年了");
//也可以从一个委托实例中删除另一个
背景 -= 小李说;
背景 -= 小王说;
背景("人没了");

namespace 委托
{
    //声明委托类型
    public static class MyDelegate
    {
        public delegate void StringProcessor(string input);
        public delegate int? ReturnStringProcessor(string input);
    }

    class Person
    {
        public string Name { get; set; }
        public Person(string name)
        {
            Name = name;
        }
        public void Say(string message)
        {
            Console.WriteLine($"{Name} says: {message}");
        }
    }

    class Background
    {
        public static void Note(string note)
        {
            Console.WriteLine("({0})", note);
        }
    }
}







