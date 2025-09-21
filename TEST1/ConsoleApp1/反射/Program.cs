
//需使用如下命名空间
using System;
using System.Reflection;


//用来访问程序元数据的 attribute

static void DemonstrateTypeof<X>()
{
    Console.WriteLine(typeof(X));
    Console.WriteLine(typeof(List<>));    //显示泛型类型
    Console.WriteLine(typeof(Dictionary<,>));

    Console.WriteLine(typeof(List<X>));     //❶ 显式封闭类型（尽管使用了类型参数）

    Console.WriteLine(typeof(Dictionary<string, X>));

    Console.WriteLine(typeof(List<long>));    //显式封闭类型
    Console.WriteLine(typeof(Dictionary<long, Guid>));
}
DemonstrateTypeof<int>();

//获取泛型和已构造Type 对象的各种方式
string listTypeName = "System.Collections.Generic.List`1";

Type defByName = Type.GetType(listTypeName);

Type closedByName = Type.GetType(listTypeName + "[System.String]");
Type closedByMethod = defByName.MakeGenericType(typeof(string));
Type closedByTypeof = typeof(List<string>);

Console.WriteLine(closedByMethod == closedByName);
Console.WriteLine(closedByName == closedByTypeof);

Type defByTypeof = typeof(List<>);
Type defByMethod = closedByName.GetGenericTypeDefinition();

Console.WriteLine(defByMethod == defByName);
Console.WriteLine(defByName == defByTypeof);

//通过反射来获取和调用泛型方法
Type type = typeof(Example);
Console.WriteLine(type.FullName);
//MethodInfo definition = type.GetMethod("PrintTypeParameter<T>()");
//Console.WriteLine(definition.IsGenericMethod);
//MethodInfo constructed = definition.MakeGenericMethod(typeof(string));
//constructed.Invoke(null, null);
class Example
{
    static void PrintTypeParameter<T>()
    {
        Console.WriteLine(typeof(T));
    }

}