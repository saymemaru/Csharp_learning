using System.Collections;
using 迭代_协程;

// 迭代器遍历字典元素
Dictionary<string, int> dic1 = new() 
    { 
        { "a", 1},
        { "b", 2},
        { "c", 3}
    };
// 1、获取字典（可迭代对象）的迭代器
var dictEnum = dic1.GetEnumerator();
// 2、MoveNext()获取当前迭代器位置的元素
//      迭代器初始位置：迭代器初始时指向集合的第一个元素之前，需要先调用 MoveNext() 才能获取第一个元素
while (dictEnum.MoveNext())
{
    KeyValuePair<string, int> item = dictEnum.Current;
    Console.Write(item.Key + " " + item.Value + " ");
}
Console.WriteLine();
// 3、等价于foreach
foreach (KeyValuePair<string, int> item in dic1)
{
    Console.Write(item.Key + " " + item.Value + " ");
}
Console.WriteLine();


People people = new(
    [
        new Person("j","k"),
        new Person("a","b"),
        new Person("g","h"),
    ]);
//foreach原理
IEnumerator iterator = people.GetEnumerator();
while (iterator.MoveNext())
    Console.WriteLine(iterator.Current);

Dinner dinner = new(
    [
        new Human(11f),
        new Human(5f),
        new Human(8.4f)
    ]);
IEnumerator dinnerIterator = dinner.GetEnumerator();
while (dinnerIterator.MoveNext())
    Console.WriteLine(dinnerIterator.Current);
