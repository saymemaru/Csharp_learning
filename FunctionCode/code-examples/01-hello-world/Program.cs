using System;
using System.Linq;
using System.Collections.Generic;

namespace FpHelloWorld;

class Program
{
    static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  C# 函数式编程 Hello World");
        Console.WriteLine("  命令式 vs 函数式 对比");
        Console.WriteLine("==================================================");

        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // ─── 命令式风格 ───
        Console.WriteLine("\n--- 命令式风格 ---");
        var imperativeResult = 0;
        var imperativeList = new List<int>();
        foreach (var n in numbers)
        {
            if (n % 2 == 0)
            {
                var squared = n * n;
                imperativeList.Add(squared);
                imperativeResult += squared;
            }
        }
        Console.WriteLine($"  筛选偶数并平方: [{string.Join(", ", imperativeList)}]");
        Console.WriteLine($"  求和: {imperativeResult}");

        // ─── 函数式风格 (LINQ) ───
        Console.WriteLine("\n--- 函数式风格 (LINQ) ---");
        var functionalList = numbers
            .Where(n => n % 2 == 0)     // 筛选
            .Select(n => n * n)         // 映射
            .ToList();
        var functionalSum = functionalList.Sum();
        Console.WriteLine($"  筛选偶数并平方: [{string.Join(", ", functionalList)}]");
        Console.WriteLine($"  求和: {functionalSum}");

        // ─── 练习1：字符串处理 ───
        Console.WriteLine("\n--- 练习1: 字符串处理 ---");
        var words = new[] { "apple", "banana", "cherry", "date", "elderberry" };
        var filtered = words
            .Where(w => w.Length >= 6)
            .Select(w => w.ToUpper())
            .OrderBy(w => w)
            .ToArray();
        Console.WriteLine($"  结果: [{string.Join(", ", filtered)}]");

        // ─── 练习2：用 Aggregate 替代 Sum ───
        Console.WriteLine("\n--- 练习2: Aggregate 模拟 Sum ---");
        var aggregateSum = numbers
            .Where(n => n % 2 == 0)
            .Select(n => n * n)
            .Aggregate((acc, x) => acc + x);
        Console.WriteLine($"  用 Aggregate 求和: {aggregateSum}");

        // ─── 更复杂的管道: 完全用函数式实现 FizzBuzz ───
        Console.WriteLine("\n--- 函数式 FizzBuzz ---");
        var fizzBuzz = Enumerable.Range(1, 30)
            .Select(n => (n % 3, n % 5) switch
            {
                (0, 0) => "FizzBuzz",
                (0, _) => "Fizz",
                (_, 0) => "Buzz",
                _      => n.ToString()
            });
        Console.WriteLine($"  {string.Join(", ", fizzBuzz)}");
    }
}
