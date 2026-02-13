//委托
// 引用方法，将方法作为参数传递，或者将方法赋值给变量

// 定义一个委托类型delegate void Dele() 		//方法的类型，代表这一类方法
// 声明一个委托类型变量 Dele dele		//方法的变量，存储方法的地址
//声明一个方法（方法返回值、变量要和委托相同）void myfunc()
//将这个方法赋给委托类型变量dele = myfunc 、dele += myfunc	// 引用方法的地址
//调用 dele() 	// 调用引用的方法

// 后+=的方法先被调用（类似栈）

// 特定的委托
// Action 无返回值的委托
// Func<> 最后一个参数为返回值，前面参数为传入值的委托
// event 封装的委托

using 委托;
using static 委托.MyDelegate;

Person 小王 = new Person("小王");
Person 小李 = new Person("小李");
StringProcessor 小王说 = 小王.Say; // 赋值
StringProcessor 小李说 = new(小李.Say); // 指向
StringProcessor 背景 = Background.Note;
小王说("啊我死了!");
小李说("你怎么死了");//隐式调用
背景?.Invoke("死人笑声");//显式调用invoke ， ？判断是否为空，不为空则执行

//委托作为参数传入方法
Background.SayTogether(小王说, 小李说);

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
            OnDead += Person_OnDead;
        }

        private void Person_OnDead()
        {
            Console.WriteLine("好死");
        }

        public void Say(string message)
        {
            Console.WriteLine($"{Name} says: {message}");
        }

        //事件
        public event Action OnDead;
        public event EventHandler OnXXXHandler;
        private void Dead()
        {
            //空值判断
            OnDead?.Invoke();

            //传递参数：调用对象（this）,可选事件参数
            OnXXXHandler?.Invoke(this, new MyEventArg() { Message = "hello"} );
            OnXXXHandler(this, EventArgs.Empty);

            //多线程，先获取引用，再调用
            //var handler = OnXXXHandler;
            //handler?.Invoke(this, EventArgs.Empty);
        }
  
    }

    class Background
    {
        public static void Note(string note)
        {
            Console.WriteLine("({0})", note);
        }

        public static void SayTogether(StringProcessor p1, StringProcessor p2)
        {
            p1("hi");
            p2("hi");
        }
    }

    /*事件（只能在本类内部调用）
    事件和委托的比较
    事件的注册方法和委托相同
    事件第一次注册也使用+=
    类的外部，无法清空委托链或者直接引发*/

    //事件参数，继承EventArgs
    public class MyEventArg : EventArgs
    {
        public object Sender {  get; set; }

        public string Message { get; set; }

        //etc.

    }
}







