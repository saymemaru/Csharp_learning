using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using 泛型;
//泛型实现了类型和方法的参数化

//泛型字典
string text = @"Do you like green eggs and ham?
               I do not like them, Sam-I-am.
               I do not like green eggs and ham.";
Dictionary<string, int> frequencies = Generic_.CountWords(text);
// 打印映射中的每个键/值对
foreach (KeyValuePair<string, int> entry in frequencies)  
{
    //使用泛型提前定义了entry的类型，就不需要再进行强制类型检查或转换
    var word = entry.Key;
    var frequency = entry.Value;
    Console.WriteLine("{0}: {1}", word, frequency);
}


//泛型类型 的 泛型方法
double TakeSquareRoot(int x)
{
    return Math.Sqrt(x);
}
string ToString(int x)
{
    return x.ToString();
}

List<int> integers = new List<int>();  /*❶ 创建并填充一个整数列表*/
integers.Add(1);
integers.Add(2);
integers.Add(3);
integers.Add(4);
//泛型委托 类型转换器Converter<TInput,TOutput>
Converter<int,double> converter1 = TakeSquareRoot;   /*❷ 创建委托实例  */
Converter<int,string> converter2 = ToString;   
List<double> doubles = integers.ConvertAll(converter1); /*❸ 调用泛型方法来转换列表*/
List<string> strings = integers.ConvertAll(converter2);
foreach (double d in doubles)
{
    Console.WriteLine(d);
}
foreach (string s in strings)
{
    Console.WriteLine(s);
}



//非泛型类型 的 泛型方法
static List<T> MakeList<T>(T first, T second)
{
    List<T> list = new ();
    list.Add(first);
    list.Add(second);
    return list;
}
List<string> list1 = MakeList<string>("Line 1", "Line 2");
List<int> list3 = MakeList(1, 2);
foreach (string x in list1)
{
    Console.WriteLine(x);
}

//被限制的泛型类型
Creator1<无参构造函数> creator1 = new();// 必须 class类型；new()约束，
Creator2<指定Class> creator2 = new (); // 必须 指定Class类
Creator3<IArmy> creator3 = new Creator3<IArmy>(); // 必须 IArmy接口
Creator4<int> creator4 = new (); // 必须 struct类型

//可比较的约束
static int CompareToDefault<T>(T value) where T : IComparable<T>
{
    return value.CompareTo(default(T));
}
Console.WriteLine(CompareToDefault("x"));
Console.WriteLine(CompareToDefault(10));
Console.WriteLine(CompareToDefault(0));
Console.WriteLine(CompareToDefault(-10));
Console.WriteLine(CompareToDefault(DateTime.MinValue));

//不知道T的具体类型，只知道它是引用类型时使用 == != 比较操作符
//对于引用类型，== 比较的是引用是否相同，而不是对象的内容
static bool AreReferencesEqual<T>(T first, T second) where T : class
{
    return first == second;   //❶ 比较引用 
}
string name = "Jon";
string intro1 = "My name is " + name;
string intro2 = "My name is " + name;
Console.WriteLine(intro1 == intro2);   //❷ 使用string重载的== 返回true
Console.WriteLine(AreReferencesEqual(intro1, intro2)); //false


//使用多个类型参数的泛型类型Creator7
Creator7<int, string> creatorA = Creator7.Of(343, "Hello");
Creator7<int, string> creatorB = new Creator7<int, string>(42, "Hello");
