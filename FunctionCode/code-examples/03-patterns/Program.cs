using System;
using System.Linq;
using System.Collections.Generic;

namespace FpPatterns;

// ══════════════════════════════════════════
// 模式 1: 不可变数据模型 (record)
// ══════════════════════════════════════════
public record Address(string Street, string City, string ZipCode);
public record Customer(int Id, string Name, string Email, Address ShippingAddress);

// ══════════════════════════════════════════
// 模式 2: Result 类型 (替代异常)
// ══════════════════════════════════════════
public abstract record Result<T, TError>;
public sealed record Success<T, TError>(T Value) : Result<T, TError>;
public sealed record Failure<T, TError>(TError Error) : Result<T, TError>;

// ══════════════════════════════════════════
// 模式 3: 领域模型 (纯函数业务逻辑)
// ══════════════════════════════════════════
public record OrderItem(string ProductName, decimal UnitPrice, int Quantity);
public record Order(IReadOnlyList<OrderItem> Items, string CouponCode);
public record Invoice(decimal SubTotal, decimal Discount, decimal Tax, decimal Total);

// Result 的 SelectMany (LINQ 查询语法支持)
public static class ResultExtensions
{
    public static Result<TOut, TError> SelectMany<T1, T2, TOut, TError>(
        this Result<T1, TError> result,
        Func<T1, Result<T2, TError>> bind,
        Func<T1, T2, TOut> project)
        => result switch
        {
            Success<T1, TError>(var v) => bind(v) switch
            {
                Success<T2, TError>(var v2) => new Success<TOut, TError>(project(v, v2)),
                Failure<T2, TError>(var e)  => new Failure<TOut, TError>(e),
                _ => throw new InvalidOperationException()
            },
            Failure<T1, TError>(var e) => new Failure<TOut, TError>(e),
            _ => throw new InvalidOperationException()
        };
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  C# 函数式编程 — 实践模式");
        Console.WriteLine("==================================================");

        // ─── 模式 1: 不可变数据 ───
        Console.WriteLine("\n=== 模式 1: 不可变数据 (record) ===");
        var address = new Address("123 Main St", "Shanghai", "200000");
        var customer = new Customer(1, "Alice", "alice@example.com", address);
        var updated = customer with
        {
            Email = "alice@newdomain.com",
            ShippingAddress = customer.ShippingAddress with { City = "Beijing" }
        };
        Console.WriteLine($"  原客户: {customer.Name}, {customer.ShippingAddress.City}");
        Console.WriteLine($"  更新后: {updated.Name}, {updated.ShippingAddress.City}");
        Console.WriteLine($"  原客户城市不变: {customer.ShippingAddress.City}");

        // ─── 模式 2: Result 类型 ───
        Console.WriteLine("\n=== 模式 2: Result 类型 ===");
        var r1 = SafeDivide(10, 2);
        var r2 = SafeDivide(10, 0);
        Console.WriteLine($"  Divide(10, 2): {FormatResult(r1)}");
        Console.WriteLine($"  Divide(10, 0): {FormatResult(r2)}");

        // 用 LINQ SelectMany 组合 Result 操作
        Console.WriteLine("\n  组合 Result 操作:");
        var composed = from x in SafeDivide(10, 2)
                       from y in SafeDivide(x, 2)
                       select y;
        Console.WriteLine($"    10/2=5, 5/2={FormatResult(composed)}");

        var failedCompose = from x in SafeDivide(10, 0)
                            from y in SafeDivide(x, 2)
                            select y;
        Console.WriteLine($"    10/0 然后 ...: {FormatResult(failedCompose)}");

        // ─── 模式 3: 纯函数业务逻辑 ───
        Console.WriteLine("\n=== 模式 3: 纯函数业务逻辑 ===");

        var order = new Order(
            new List<OrderItem>
            {
                new("Laptop", 5000m, 1),
                new("Mouse", 200m, 2),
                new("Keyboard", 800m, 1)
            },
            "SAVE10"
        );

        var invoice = GenerateInvoice(order);
        Console.WriteLine($"  订单明细:");
        foreach (var item in order.Items)
            Console.WriteLine($"    {item.ProductName}: {item.UnitPrice,8:C} x {item.Quantity}");
        Console.WriteLine($"  ──────────────────────");
        Console.WriteLine($"  小计:     {invoice.SubTotal,8:C}");
        Console.WriteLine($"  折扣:    -{invoice.Discount,8:C}");
        Console.WriteLine($"  税费:     {invoice.Tax,8:C} (13%)");
        Console.WriteLine($"  总计:     {invoice.Total,8:C}");

        // ─── 模式 4: 函数柯里化与 partial application ───
        Console.WriteLine("\n=== 模式 4: 柯里化与 Partial Application ===");
        var curriedAdd = CurryAdd();
        var add5 = curriedAdd(5);
        var add10 = curriedAdd(10);
        Console.WriteLine($"  curriedAdd(5)(3) = {add5(3)}");
        Console.WriteLine($"  curriedAdd(10)(3) = {add10(3)}");

        // ─── 模式 5: 管道式数据处理 ───
        Console.WriteLine("\n=== 模式 5: 管道式数据处理 ===");
        var data = new[] { 3, 7, 1, 9, 4, 6, 8, 2, 5 };
        var pipeline = data
            .Where(x => x > 3)
            .OrderByDescending(x => x)
            .Select((x, i) => $"#{i + 1}: {x}")
            .ToList();

        Console.WriteLine("  数据管道结果:");
        pipeline.ForEach(x => Console.WriteLine($"    {x}"));

        // ─── 综合 ───
        Console.WriteLine("\n=== 综合: 函数式风格完整示例 ===");
        Console.WriteLine("  5000 以内所有奇数的平方和:");
        var result = Enumerable.Range(1, 1000)
            .Where(n => n % 2 == 1)
            .Select(n => n * n)
            .Sum();
        Console.WriteLine($"  {result:N0}");
    }

    // ─── Result 模式 — 安全的除法 ───
    static Result<decimal, string> SafeDivide(decimal a, decimal b)
        => b == 0
            ? new Failure<decimal, string>("除数不能为零")
            : new Success<decimal, string>((decimal)a / b);

    static string FormatResult(Result<decimal, string> result) => result switch
    {
        Success<decimal, string>(var v) => $"{v:F2}",
        Failure<decimal, string>(var e) => $"[错误] {e}",
        _ => "未知"
    };

    // ─── 纯函数 — 业务逻辑 ───
    static decimal CalcSubTotal(Order order)
        => order.Items.Sum(i => i.UnitPrice * i.Quantity);

    static decimal CalcDiscount(decimal subTotal, string coupon) => coupon switch
    {
        "SAVE10" => subTotal * 0.10m,
        "SAVE20" => subTotal * 0.20m,
        "" or null => 0m,
        _ => 0m
    };

    static decimal CalcTax(decimal afterDiscount) => afterDiscount * 0.13m;

    static Invoice GenerateInvoice(Order order)
    {
        var subTotal = CalcSubTotal(order);
        var discount = CalcDiscount(subTotal, order.CouponCode);
        var afterDiscount = subTotal - discount;
        var tax = CalcTax(afterDiscount);
        var total = afterDiscount + tax;
        return new Invoice(subTotal, discount, tax, total);
    }

    // ─── 柯里化 ───
    static Func<int, Func<int, int>> CurryAdd()
        => a => b => a + b;
}


