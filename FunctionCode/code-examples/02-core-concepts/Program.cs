using System;
using System.Linq;
using System.Collections.Generic;

namespace FpCoreConcepts;

// 1. 不可变性 — 使用 record 定义不可变数据模型
public record Person(string Name, int Age);

// 2. Option 类型 — 用于演示模式匹配和纯函数
public abstract record Option<T>;
public sealed record Some<T>(T Value) : Option<T>;
public sealed record None<T> : Option<T>;

class Program
{
    static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  C# 函数式编程 — 核心概念");
        Console.WriteLine("==================================================");

        // ─── 1. 纯函数 ───
        Console.WriteLine("\n=== 1. 纯函数 ===");
        Console.WriteLine($"  加法: Add(3, 7) = {Add(3, 7)}");
        Console.WriteLine($"  加法: Add(-5, 10) = {Add(-5, 10)}");
        Console.WriteLine("  (相同输入永远相同输出, 无副作用)");

        // ─── 2. 不可变性 ───
        Console.WriteLine("\n=== 2. 不可变性 ===");
        var p1 = new Person("Alice", 30);
        var p2 = p1 with { Age = 31 }; // 创建新实例, 原数据不变
        Console.WriteLine($"  p1: {p1}");
        Console.WriteLine($"  p2 (从 p1 with Age=31): {p2}");
        Console.WriteLine($"  p1 == p2: {p1 == p2} (值相等性)");

        // 不可变集合
        var original = new List<int> { 1, 2, 3 }.AsReadOnly();
        // original.Add(4);  // 编译错误: AsReadOnly 返回只读视图
        Console.WriteLine($"  只读集合: [{string.Join(", ", original)}]");

        // ─── 3. 高阶函数 ───
        Console.WriteLine("\n=== 3. 高阶函数 ===");
        var numbers = new[] { 1, 2, 3, 4, 5 };
        var doubled = Transform(numbers, x => x * 2);
        var squared = Transform(numbers, x => x * x);
        Console.WriteLine($"  Transform(x => x*2): [{string.Join(", ", doubled)}]");
        Console.WriteLine($"  Transform(x => x*x): [{string.Join(", ", squared)}]");

        // 返回函数的函数
        var add5 = MakeAdder(5);
        Console.WriteLine($"  MakeAdder(5)(10) = {add5(10)}");
        var add20 = MakeAdder(20);
        Console.WriteLine($"  MakeAdder(20)(10) = {add20(10)}");

        // 闭包
        var counter = MakeCounter();
        Console.WriteLine($"  闭包计数器: {counter()} {counter()} {counter()}");

        // ─── 4. 函数组合 ───
        Console.WriteLine("\n=== 4. 函数组合 ===");
        Func<int, int> add1 = x => x + 1;
        Func<int, int> mult2 = x => x * 2;

        // 手工组合
        var add1ThenMult2 = Compose(add1, mult2);
        Console.WriteLine($"  Compose(x+1, x*2)(3) = {add1ThenMult2(3)}"); // (3+1)*2 = 8

        var mult2ThenAdd1 = Compose(mult2, add1);
        Console.WriteLine($"  Compose(x*2, x+1)(3) = {mult2ThenAdd1(3)}"); // (3*2)+1 = 7

        // LINQ 本身就是组合
        var pipeline = numbers
            .Select(x => x + 1)
            .Select(x => x * 2)
            .ToArray();
        Console.WriteLine($"  LINQ 管道: [{string.Join(", ", pipeline)}]");

        // ─── 5. 模式匹配 ───
        Console.WriteLine("\n=== 5. 模式匹配 ===");
        Console.WriteLine($"  Describe(42): {Describe(42)}");
        Console.WriteLine($"  Describe(-5): {Describe(-5)}");
        Console.WriteLine($"  Describe(\"hello\"): {Describe("hello")}");
        Console.WriteLine($"  Describe(null): {Describe(null)}");
        Console.WriteLine($"  Describe(3.14): {Describe(3.14)}");

        // 元组模式匹配
        Console.WriteLine($"\n  点分类:");
        Console.WriteLine($"    (0, 0) -> {ClassifyPoint(0, 0)}");
        Console.WriteLine($"    (0, 5) -> {ClassifyPoint(0, 5)}");
        Console.WriteLine($"    (3, 0) -> {ClassifyPoint(3, 0)}");
        Console.WriteLine($"    (2, 4) -> {ClassifyPoint(2, 4)}");

        // Option + 模式匹配
        Console.WriteLine($"\n  安全的解析整数:");
        var parsed1 = ParseInt("42");
        var parsed2 = ParseInt("not-a-number");
        Console.WriteLine($"    ParseInt(\"42\") -> {FormatOption(parsed1)}");
        Console.WriteLine($"    ParseInt(\"not-a-number\") -> {FormatOption(parsed2)}");
    }

    // ─── 纯函数 ───
    static int Add(int a, int b) => a + b;

    // ─── 高阶函数: 接受函数参数 ───
    static int[] Transform(int[] numbers, Func<int, int> transformer)
        => numbers.Select(transformer).ToArray();

    // ─── 高阶函数: 返回函数 ───
    static Func<int, int> MakeAdder(int offset)
        => x => x + offset;

    // ─── 闭包 ───
    static Func<int> MakeCounter()
    {
        int count = 0;
        return () => count++;
    }

    // ─── 函数组合 ───
    static Func<T, TOut2> Compose<T, TOut1, TOut2>(
        Func<T, TOut1> f, Func<TOut1, TOut2> g)
        => x => g(f(x));

    // ─── 模式匹配 ───
    static string Describe(object obj) => obj switch
    {
        int i when i < 0 => $"负数 {i}",
        int i            => $"正整数 {i}",
        string s         => $"字符串 \"{s}\"",
        null             => "空引用",
        _                => $"未知类型 ({obj?.GetType().Name ?? "null"})"
    };

    static string ClassifyPoint(int x, int y) => (x, y) switch
    {
        (0, 0) => "原点",
        (0, _) => "Y 轴上的点",
        (_, 0) => "X 轴上的点",
        _      => $"点 ({x}, {y})"
    };

    // ─── Option 用法 ───
    static Option<int> ParseInt(string s)
        => int.TryParse(s, out var n) ? new Some<int>(n) : new None<int>();

    static string FormatOption(Option<int> opt) => opt switch
    {
        Some<int>(var v) => $"Some({v})",
        None<int>        => "None",
        _                => "未知"
    };
}
