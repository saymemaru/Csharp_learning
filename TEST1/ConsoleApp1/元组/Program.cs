// 原文 https://www.cnblogs.com/cdaniu/p/15212967.html

// 元组是具有特定数量和元素序列的数据结构
// Tuple类型像一个口袋，在出门前可以把所需的任何东西一股脑地放在里面。

//一、引用元组 Tuple
//调用 Create 方法或 new 创建
var primes = Tuple.Create(2, 3, 5, 7, 11, 13, 17, 19);
var population = new Tuple<string, int, int, int, int, int, int>(
                           "New York", 7891957, 7781984,7894862, 7071639, 7322564, 8008278);

//二、值元组 ValueTuple
//元组写法1通过example2.Item来引用
var example1 = (1f, 2d, 3m, 4u, 5, "23", 1L, 2);
Console.WriteLine(example1.Item4);

//元组写法2, 通过example3.变量名引用
var example2 = (exa1: 1, exa2: 2, 3, 4, 5, 6);
Console.WriteLine(example2.exa2);

//元组写法3, 通过example3.变量名引用  左侧不允许弃元
(int age, string name) example3 = (3, "Dog3");
Console.WriteLine(example3.name);

//元组写法4 相当于批量赋值 可以单独使用 变量 左侧不允许弃元
(string sr, bool sb, int sc) = ("4sr", true, 1);
Console.WriteLine(sr);

//元组写法5 元组元素是公共字段 所以可以单独引用
var (exa51, exa52) = ("51f", 5.1);
Console.WriteLine(exa51);

//元组写法6 元组元素是公共字段 所以可以单独引用
var example6 = ("post office", 6.3);
(string destination, double distance) = example6;
Console.WriteLine(distance);

//元组写法7 将元组分配到各个已预声明的变量中。
var exa71 = string.Empty;
var exa72 = 0.0;
var example7 = ("post office", 7.2);
(exa71, exa72) = example7;
Console.WriteLine(exa72);

//元组写法8  将元组分配到各个已预声明的变量中。

string country;
string capital;
double gdpPerCapita;
(country, capital, gdpPerCapita) = ("Malawi", "Lilongwe", 226.50);
System.Console.WriteLine($"The poorest country in the world in 2017 was {country}, {capital}: {gdpPerCapita}");

//元组写法9 _下划线弃元 将未命名的元组分配到一个隐式类型化变量中,
var countrInfo = ("Malawi", "Lilongwe", 226.50);

(string name, _, double gdpPerCapit) = countrInfo;
Console.WriteLine(gdpPerCapit);
//C#10混合定义
int y = 0;
(var x, y, var z) = (1, 2, 3);


// 三、比较运算符！= 和== 从 C# 7.3 开始支持

// 四、元组作为 out 参数
// https://docs.microsoft.com/zh-cn/archive/msdn-magazine/2017/august/essential-net-csharp-7-0-tuples-explained
