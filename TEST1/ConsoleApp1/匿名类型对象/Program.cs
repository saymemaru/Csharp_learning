using System.Reflection;
//一、匿名类型
//编译器编译时动态生成一个类，只读，只在当前作用域内可见
//通常用于临时存储数据，避免定义一个类
var newPerson = new { Name = "Alice", Age = 30 };
Console.WriteLine($"Name: {newPerson.Name}, Age: {newPerson.Age}");

//二、动态类型处理匿名类型对象
dynamic ReturnDynamicObject()
{
    return new { Name = "Bob", Age = 25 };
}
dynamic dynamicPerson = ReturnDynamicObject();
Console.WriteLine($"接收了:Name: {dynamicPerson.Name}, Age: {dynamicPerson.Age}");
//注意：dynamic类型在编译时跳过类型检查，运行时才解析成员，可能导致运行时错误
//！！！错误的成员名不会显示在错误提示中！！！运行时仍会报错
//Console.WriteLine($"Name: {dynamicPerson.Name11}, Age: {dynamicPerson.Age}");

//三、指定匿名类型接收匿名类型对象
var person = new { Name = "", Age = 0 };
person = ReturnDynamicObject();
Console.WriteLine($"接收了:Name: {person.Name}, Age: {person.Age}");

//四、通过反射访问匿名类型对象成员
//获取所有成员
void GetAllAnonymousTypeProperties(object AnonymousObj)
{
    Type type = AnonymousObj.GetType();
    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);//当前只能访问公共实例成员
    foreach (var prop in properties)
    {
        Console.WriteLine($"{prop.Name}: {prop.GetValue(AnonymousObj)}");
    }
}
//获取特定成员
void GetAnonymousTypeProperty(object AnonymousObj)
{
    Type type = AnonymousObj.GetType();
    PropertyInfo? propertyName = type.GetProperty("Age");
    Console.WriteLine($"获取特定字段：{propertyName?.Name}:{propertyName?.GetValue(AnonymousObj)}");

}
GetAllAnonymousTypeProperties(newPerson);
GetAnonymousTypeProperty(newPerson);

//五、linq查询匿名类型
var people = new[]
{
    new { Name = "Alice", Age = 30 },
    new { Name = "Bob", Age = 25 },
    new { Name = "Charlie", Age = 35 }
};
var query = from p in people
            where p.Age > 28
            select p;
Console.WriteLine("Linq查询结果:[Age大于28的人]");
foreach (var p in query)
{
    Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
}