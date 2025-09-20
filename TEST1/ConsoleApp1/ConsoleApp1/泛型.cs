using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class 泛型
    {
        //泛型方法
        public static void Add<T>(T item) { }

        //泛型接口
        interface ICreate<T> { } //定义泛型接口

        //泛型类
        class Creator : ICreate<string> { } //实现泛型接口时，必须指定类型参数

        //限制条件
        //只允许 T 为特定的引用类型
        class Creator1<T> where T : class, new() { }  //new()约束，表示必须有无参构造函数
        class Creator2<T> where T : class { } //
        class Creator3<T> where T : IArmy { } //接口约束，T必须实现IArmy接口

        /*    
        Use out for output
        Use in for input
        Don’t use any if you have both
        */
        //协变 输入小类型，返回大类型
        interface ICovariance<out T> 
        {
            public T GetValue();
        }

        //逆变，输入大类型，返回小类型
        interface IContravariant<in Object> 
        {
            public string GetValue();
        }

        //泛型与非泛型方法的对比
        //如果，你想创建一个可以处理多种数据类型的方法或类，泛型是一个非常有用的工具。
        //但是，过度使用泛型可能会导致代码复杂化和可读性降低
        //除非，你确实需要处理特定的多种数据类型
        public static void LogBad<T>(T message)
        {
            Console.WriteLine(message.ToString());
        }
        //如果，你不需要泛型的灵活性，使用如下非泛型方法可能更简单和直接。
        public static void LogGood(Object message)
        {
            Console.WriteLine(message.ToString());
        }

    }
}
