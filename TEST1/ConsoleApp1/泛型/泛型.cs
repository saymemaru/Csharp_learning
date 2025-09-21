using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static 泛型.Generic_;

namespace 泛型
{
    //泛型接口
    interface ICreate<T> { } //定义泛型接口
    //泛型类
    class Creator : ICreate<string> { } //实现泛型接口时，必须指定类型参数

    //限制条件 constraint
    //目的：我们想制定规则，从而判断哪些是泛型类型或泛型方法能接受的有效类型实参
    //方法：只允许 T 为特定的类型，加逗号，添加多个约束
    //
    //约束可以出现在类、接口、结构或方法的声明中
    //划分为主要约束、次要约束、构造函数约束。
    //主要约束可以为引用类型约束、值类型约束或使用类的转换类型约束。只能有一个
    //次要约束为使用接口或其他类型参数的转换类型约束。可以有多个，
    //构造函数约束也是可选的（如果拥有了值类型约束，就不能再使用构造函数约束）。
    class Creator1<T> where T : class, new() { }  //class；new()约束，表示必须有无参构造函数
    class Creator2<T> where T : 指定Class { } //指定Class
    class Creator3<T> where T : IArmy { } //指定T包含 IArmy接口
    class Creator4<T> where T : struct { } //struct
    class Creator5<T, U> where T : class where U : struct, T { } //多个类型参数
    class Creator6<T, U> where T : U { }

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


    public class Generic_
    {
        //泛型方法，用T指定想要使用的类型
        public static void Add<T>(T item) { }

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

        //泛型字典
        // 下面的CountWords方法接受一个字符串参数，并返回一个字典，字典的键是单词，值是该单词在文本中出现的次数
        public static Dictionary<string, int> CountWords(string text)
        {
            Dictionary<string, int> frequencies = new Dictionary<string, int>(); ;   //❶ 创建从单词到频率的新映射 

            string[] words = Regex.Split(text, @"\W+");   //❷ 将文本分解成单词

            foreach (string word in words)
            {
                if (frequencies.ContainsKey(word))  /*❸ 添加或更新映射*/
                {
                    frequencies[word]++;
                }
                else
                {
                    frequencies[word] = 1;
                }
            }
            return frequencies;
        }

    }

    //实现带有两个泛型参数IEquatable<T>接口，重写Equals方法
    //用于比较两个Creator7实例是否相等
    public sealed class Creator7<T1, T2> : IEquatable<Creator7<T1, T2>>
    {
        private static readonly IEqualityComparer<T1> FirstComparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> SecondComparer = EqualityComparer<T2>.Default;

        private readonly T1 first;
        private readonly T2 second;

        public Creator7(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 First { get { return first; } }

        public T2 Second { get { return second; } }

        public bool Equals(Creator7<T1, T2> other)
        {
            return other != null &&
            FirstComparer.Equals(this.First, other.First) &&
            SecondComparer.Equals(this.Second, other.Second);
        }
        public override bool Equals(object o)
        {
            return Equals(o as Creator7<T1, T2>);
        }
        public override int GetHashCode()
        {
            return FirstComparer.GetHashCode(first) * 37 +
               SecondComparer.GetHashCode(second);
        }
    }

    //辅助类，创建Creator7实例
    public static class Creator7
    {

        //使用包含泛型方法的非泛型类型进行类型推断（不用new()Creator7<T1, T2>了！）
        public static Creator7<T1, T2> Of<T1, T2>(T1 first, T2 second)
        {
            return new Creator7<T1, T2>(first, second);
        }
    }

    //演示用
    public class 无参构造函数
    {
    }
    public class 指定Class
    {
        private string _name;
        指定Class(string name) 
        {
            _name = name;
        }
    }
    internal interface IArmy
    {

    }
}
