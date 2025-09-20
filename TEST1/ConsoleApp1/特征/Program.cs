using 特征;
using System;
using System.Reflection;

Person person = new Person();

//获取person的类型
Type type = person.GetType();

//获取Person类的Attribute
var attributes = type.GetCustomAttributes(true);//true表示获取所有继承的特性
var student1 = type.GetCustomAttribute(typeof(StudentAttribute)) as StudentAttribute;
var student2 =type.GetCustomAttribute<StudentAttribute>();//反射获取特性

Console.WriteLine($"Person类的特性数量：{attributes.Length}");

int i = 0;
//展示Person类的所有Attribute
foreach (var attribute in attributes)
{
    Console.WriteLine($"attributes[{i}]：{attribute}");

    //发现并展示StudentAttribute的内容
    if (attribute is StudentAttribute)
    {
        var student3 = (StudentAttribute)attribute;
        var student4 = attribute as StudentAttribute;

        Console.WriteLine($"ClassName: {student3.ClassName}");
        Console.WriteLine($"ClassNumber: {student4.ClassNumber}");
    }

    i++;
}

//Obsolete特性，将元素标记为过时，并附加警告信息
person.Sleep(10);
