using 对象与类;
using System.Collections;
//类属性实例化
人类 小王 = new()
{
    姓名 = "小王",
    性别 = "男",
    身高 = 175,
    体重 = 60,
    三种联系方式 = ["122413", "322144", "232334"]
};
Console.WriteLine($"{小王.姓名}{小王.年龄}岁了");
小王.显示生日();
//手动消灭小王
小王.Dispose();


//列表按年龄排序（修改原列表）
List<人类> peopleList1 = 人类.GetPeople();
peopleList1.Sort((x, y) => x.年龄.CompareTo(y.年龄));
Console.WriteLine();
Console.Write("按年龄排序的列表：");
foreach (var person in peopleList1)
{
    Console.Write($"{person.姓名}：{person.年龄}，");
}
Console.WriteLine();


List<人类> peopleList2 = 人类.GetPeople();
//输出年龄排序（仅获取顺序）
Console.Write("按年龄顺序输出：");
foreach (人类 person in peopleList2.OrderBy(p => p.年龄))
{
    Console.Write($"{person.年龄}，");
}
Console.WriteLine();
//输出大于25岁
Console.Write("年龄大于25：");
foreach (人类 person in peopleList2.Where(p => p.年龄 > 25))
{
    Console.Write($"{person.年龄}");
}
Console.WriteLine();


