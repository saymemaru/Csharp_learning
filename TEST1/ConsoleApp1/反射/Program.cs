
//需使用如下命名空间
using System;
using System.Reflection;

//一、调用私有成员

Example example = new Example();
Type type1 = example.GetType();//通过实例获取 Type 对象
Type type2 = typeof(Example);//通过类型获取 Type 对象

BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;//指定访问标志,当前只能访问私有实例成员
//获取私有字段
FieldInfo? field = type1.GetField("name", flags);
field?.SetValue(example, "Hello everyone!");//设置实例私有字段的值(field? 检查是否为空)
Console.WriteLine($"私有字段的值：{field?.GetValue(example)}");//获取私有字段的值
//获取私有属性
PropertyInfo? property = type1.GetProperty("Age", flags);
property?.SetValue(example, 20);//设置实例私有属性的值
Type? propertyType = property?.GetType();//获取属性类型
Console.WriteLine($"私有属性类型：{propertyType}，私有属性修改后的值:{property.GetValue(example)}");
//获取私有方法
MethodInfo? method = type1.GetMethod("Show", flags);
method?.Invoke(example, null);//调用实例私有方法(method? 检查是否为空，非空则执行)

//获取私有静态方法
flags = BindingFlags.NonPublic | BindingFlags.Static;//指定访问标志,当前只能访问私有静态成员
MethodInfo? staticMethod = type1.GetMethod("IAmStaticMethod", flags);
staticMethod?.Invoke(null, null);//调用私有静态方法，静态方法实例参数可设为 null


//二、用来访问程序元数据的 attribute
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

//通过 attribute 获取泛型和已构造Type对象的各种方式
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

    private string name = "Hello Reflection!";

    private int Age { get; set; } = 18;
    private void Show()
    {
        Console.WriteLine(name);
    }

    private static void IAmStaticMethod()
    {
        Console.WriteLine("This is a static method.");
    }
}