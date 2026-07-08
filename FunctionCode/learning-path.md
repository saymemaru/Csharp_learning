# C# 函数式编程 — 学习路径

> 系统化学习 C# 中的函数式编程：从 "为什么" 到 "怎么用" 再到 "深入精进"。

---

## Level 1: 概述与动机

> 目标：理解**为什么**要在 C# 中学习函数式编程。

### 编程范式简史

在面向对象编程 (OOP) 占据主流的 C# 世界里，函数式编程 (FP) 提供了一种不同的思维方式：

| | OOP | FP |
|---|---|---|
| 核心单元 | 对象（状态 + 行为） | 函数（变换） |
| 数据流向 | 对象间消息传递 | 数据经过函数管道 |
| 状态管理 | 可变状态（修改字段） | 不可变数据（返回新值） |
| 副作用 | 方法天然有副作用 | 隔离副作用到边界 |

### 函数式编程解决了什么问题

- **可预测性**：纯函数在相同输入下总是返回相同输出，没有隐式依赖
- **可测试性**：纯函数无需 mock，输入 -> 断言 -> 输出
- **并发安全**：不可变数据天然线程安全
- **代码复用**：高阶函数和组合让逻辑碎片化降低
- **调试友好**：函数调用链清晰，每个变换独立可检查

### 什么时候适合用函数式编程

- 数据处理管道（ETL、报表、转换）
- 业务规则引擎（输入 -> 规则 -> 输出）
- 并行/并发计算（无共享可变状态）
- 状态机与工作流（通过不可变数据表示状态转换）
- LINQ 重度使用的场景

### 什么时候不适合

- 对性能极端敏感且需要大量对象复用（避免 GC 压力）
- 与大量外部有副作用的系统集成（数据库、文件 I/O）时，函数式风格需要额外封装
- 团队没有至少 1-2 人熟悉函数式概念，强行使用会导致代码难以维护

### C# 中的函数式特性概览

C# 从 3.0 版本开始逐步引入函数式特性：

| 版本 | 函数式特性 |
|------|-----------|
| C# 3.0 | Lambda 表达式、LINQ、扩展方法、匿名类型 |
| C# 6.0 | 只读自动属性、表达式体成员 |
| C# 7.0 | 元组、模式匹配、本地函数、弃元 |
| C# 8.0 | 可空引用类型、模式表达式增强、using 声明 |
| C# 9.0 | 记录 (record)、with 表达式、顶级语句、模式匹配增强 |
| C# 10+ | record struct、全局 using、扩展属性模式 |

**关键认知**：C# 不是纯函数式语言（如 Haskell），也不是以函数式为首要范式的语言（如 F#），但你完全可以在 C# 中以函数式风格写出清晰、健壮的代码。

---

## Level 2: Hello World

> 目标：用 C# 写出第一段函数式风格的代码，感受范式差异。

### 环境准备

```bash
# 确认 .NET SDK 版本
dotnet --version  # 需要 8.0+

# 创建控制台项目
dotnet new console -n FpHelloWorld
cd FpHelloWorld
```

### 对比：命令式 vs 函数式

**问题**：将一个整数列表中的偶数筛选出来，取平方，然后求和。

#### 命令式风格

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };
var result = 0;
foreach (var n in numbers)
{
    if (n % 2 == 0)
    {
        result += n * n;
    }
}
Console.WriteLine(result);  // 56
```

> 逐个遍历、手动累加、原地修改 `result` 变量。

#### 函数式风格

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };
var result = numbers
    .Where(n => n % 2 == 0)     // 筛选偶数
    .Select(n => n * n)         // 映射为平方
    .Sum();                     // 求和

Console.WriteLine(result);  // 56
```

> 数据通过管道流动，每个步骤是一个纯变换，没有可变变量。

### 练习

1. 用 LINQ 函数式风格完成：`["apple", "banana", "cherry"]` — 筛选出长度 >= 6 的字符串，转为大写，排序后输出
2. 尝试用 `.Aggregate()` 方法替换上面的 `.Sum()`，理解聚合函数式操作

### 验证

运行 `dotnet run`，检查输出是否符合预期。

完整代码见 [code-examples/01-hello-world/](code-examples/01-hello-world/)。

---

## Level 3: 核心概念

> 目标：理解函数式编程的 5 个核心概念，以及如何在 C# 中实现它们。

### 1. 纯函数 (Pure Functions)

**定义**：相同输入永远得到相同输出，且没有可观察的副作用。

```csharp
// 纯函数
int Add(int a, int b) => a + b;

// 非纯函数 - 依赖外部状态
private int _offset = 10;
int AddWithOffset(int a, int b) => a + b + _offset;

// 非纯函数 - 有副作用
void LogAndAdd(int a, int b)
{
    Console.WriteLine($"Adding {a} and {b}");  // I/O 副作用
    // 没有返回值也是副作用的表现
}
```

**常见错误**：在纯函数内部修改输入参数、访问数据库/文件/网络、调用 `DateTime.Now`。

### 2. 不可变性 (Immutability)

**定义**：数据一旦创建就不应该被修改。

```csharp
// 可变方式
var list = new List<int> { 1, 2, 3 };
list.Add(4);  // 修改了原列表

// 不可变方式 - C# 9.0 record
record Person(string Name, int Age);
var p1 = new Person("Alice", 30);
var p2 = p1 with { Age = 31 };  // 创建新实例，p1 不变

// 不可变集合
var immutableList = new List<int> { 1, 2, 3 }.AsReadOnly();
// 或者使用 ImmutableArray（需要 System.Collections.Immutable NuGet 包）
```

**常见错误**：在方法内部修改参数对象的属性；认为 `readonly` 修饰的引用类型不可变（它只保证引用不变）。

### 3. 高阶函数 (Higher-Order Functions)

**定义**：接受函数作为参数，或者返回函数作为结果的函数。

```csharp
// C# 中用委托 / Func<> / Action<> 实现高阶函数

// 接受函数参数
int[] Transform(int[] numbers, Func<int, int> transformer)
    => numbers.Select(transformer).ToArray();

var doubled = Transform(new[] { 1, 2, 3 }, x => x * 2); // [2, 4, 6]

// 返回函数
Func<int, int> MakeAdder(int offset)
    => x => x + offset;

var add5 = MakeAdder(5);
Console.WriteLine(add5(10));  // 15

// 闭包 (Closure) - 函数捕获其创建环境中的变量
Func<int> MakeCounter()
{
    int count = 0;
    return () => count++;  // 捕获了 count 变量
}
var counter = MakeCounter();
Console.WriteLine(counter());  // 0
Console.WriteLine(counter());  // 1

// 使用 C# 本地函数实现局部高阶逻辑
int ApplyTwice(Func<int, int> f, int x) => f(f(x));
var result = ApplyTwice(x => x + 1, 5);  // 7
```

**常见错误**：忽略闭包捕获变量的生命周期；捕获循环变量导致意外行为（C# 5.0+ 已修复，但理解机制仍有价值）。

### 4. 函数组合 (Function Composition)

**定义**：将多个小函数组合成一个更大的函数。

```csharp
// 简单组合
Func<int, int> add1 = x => x + 1;
Func<int, int> multiply2 = x => x * 2;

// 手动定义组合函数
Func<int, int> Add1ThenMultiply2 = x => multiply2(add1(x));
Console.WriteLine(Add1ThenMultiply2(3));  // (3+1)*2 = 8

// 通用组合扩展方法
static class FuncExtensions
{
    public static Func<T, TOut2> Compose<T, TOut1, TOut2>(
        this Func<T, TOut1> f,
        Func<TOut1, TOut2> g)
        => x => g(f(x));
}

var pipeline = add1.Compose(multiply2);
Console.WriteLine(pipeline(3));  // 8

// LINQ 本身就是组合的体现
var result = new[] { 1, 2, 3 }
    .Select(x => x + 1)      // [2, 3, 4]
    .Select(x => x * 2)      // [4, 6, 8]
    .ToArray();
```

**常见错误**：组合顺序混淆 (`f.Compose(g)` 先应用 f 还是 g)；组合链过长导致调试困难。

### 5. 模式匹配 (Pattern Matching)

**定义**：根据数据的结构或值来分支执行逻辑，比传统的 `if/switch` 更简洁安全。

```csharp
// C# 7.0+ 的模式匹配
string Describe(object obj) => obj switch
{
    int i when i < 0 => $"负数 {i}",
    int i            => $"正整数 {i}",
    string s         => $"字符串 \"{s}\"",
    null             => "空引用",
    _                => "未知类型"
};

// 位置模式 - 匹配元组
string Classify((double x, double y) point) => point switch
{
    (0, 0) => "原点",
    (0, _) => "Y 轴",
    (_, 0) => "X 轴",
    _      => $"点 ({point.x}, {point.y})"
};

// 属性模式 - 匹配 record
record Order(int Id, decimal Amount, bool IsPaid);
string GetStatus(Order order) => order switch
{
    { IsPaid: true } => $"订单 #{order.Id} 已支付",
    { Amount: 0 }    => "免费订单，无需支付",
    _                => $"订单 #{order.Id} 待支付"
};
```

**常见错误**：模式顺序导致死代码（首个匹配的优先）；遗漏 `_`（弃元模式）导致编译器警告。

### 概念关系图

```
纯函数 ── 保证可预测性
    |
    为 ── 函数组合提供基础
    |
不可变性 ── 让数据变换安全
    |
    被 ── 高阶函数 操作和组合
    |
模式匹配 ── 让分支逻辑更接近数学定义
```

---

## Level 4: 实践模式

> 目标：掌握日常 C# 开发中最常用的函数式模式。

### 模式 1：用 record 实现不可变数据模型

```csharp
// 定义不可变领域模型
public record Customer(
    int Id,
    string Name,
    string Email,
    Address ShippingAddress
);

public record Address(
    string Street,
    string City,
    string ZipCode
);

// 使用 with 表达式派生新状态
var customer = new Customer(1, "Alice", "alice@example.com",
    new Address("123 Main St", "Shanghai", "200000"));

var updatedCustomer = customer with
{
    Email = "alice@newdomain.com",
    ShippingAddress = customer.ShippingAddress with
    {
        City = "Beijing"
    }
};

// 值相等性：record 按值比较
var same = customer with { };  // 完全复制
Console.WriteLine(customer == same);  // true
```

### 模式 2：Option / Maybe 类型 — 替代 null

C# 8.0+ 的可空引用类型提供了基础，但函数式 Option 更强大：

```csharp
// 自己实现简单的 Option<T>
public abstract record Option<T>;
public sealed record Some<T>(T Value) : Option<T>;
public sealed record None<T> : Option<T>;

// 使用
static Option<int> ParseInt(string s)
    => int.TryParse(s, out var n) ? new Some<int>(n) : new None<int>();

static Option<int> SafeDivide(int a, int b)
    => b == 0 ? new None<int>() : new Some<int>(a / b);

// 管道式用法（手动实现）
static Option<TResult> Map<T, TResult>(
    this Option<T> opt, Func<T, TResult> f)
    => opt switch
    {
        Some<T>(var value) => new Some<TResult>(f(value)),
        None<T>            => new None<TResult>()
    };

var result = ParseInt("42")
    .Map(x => x * 2)
    .Map(x => x.ToString());

Console.WriteLine(result);  // Some(84)

// 生产环境建议使用 Language-Ext 库的 Option<T>
```

### 模式 3：Either / Result 类型 — 代替异常

```csharp
public abstract record Result<T, TError>;
public sealed record Success<T, TError>(T Value) : Result<T, TError>;
public sealed record Failure<T, TError>(TError Error) : Result<T, TError>;

// 使用
Result<int, string> Divide(int a, int b)
    => b == 0
        ? new Failure<int, string>("除数不能为零")
        : new Success<int, string>(a / b);

// 管道式错误处理
Result<int, string> ValidatePositive(int x)
    => x <= 0
        ? new Failure<int, string>("必须为正数")
        : new Success<int, string>(x);

// 组合
var result = from a in Divide(10, 2)
             from b in ValidatePositive(a)
             from c in Divide(b, 1)
             select c;

Console.WriteLine(result);  // Success(5)
```

### 模式 4：纯函数式业务逻辑

```csharp
// 业务逻辑 = 纯函数链

// 1. 定义不可变输入
public record OrderItem(string ProductName, decimal UnitPrice, int Quantity);
public record Order(IReadOnlyList<OrderItem> Items, string CouponCode);
public record Invoice(decimal SubTotal, decimal Discount, decimal Tax, decimal Total);

// 2. 每个步骤是纯函数
static decimal CalculateSubTotal(Order order)
    => order.Items.Sum(item => item.UnitPrice * item.Quantity);

static decimal CalculateDiscount(decimal subTotal, string couponCode)
    => couponCode switch
    {
        "SAVE10" => subTotal * 0.10m,
        "SAVE20" => subTotal * 0.20m,
        _        => 0m
    };

static decimal CalculateTax(decimal afterDiscount)
    => afterDiscount * 0.13m;  // 13% HST

// 3. 组合为完整流程
static Invoice GenerateInvoice(Order order)
{
    var subTotal = CalculateSubTotal(order);
    var discount = CalculateDiscount(subTotal, order.CouponCode);
    var afterDiscount = subTotal - discount;
    var tax = CalculateTax(afterDiscount);
    var total = afterDiscount + tax;

    return new Invoice(subTotal, discount, tax, total);
}

// 4. 测试变得极其简单
var order = new Order(
    new List<OrderItem>
    {
        new("Laptop", 5000m, 1),
        new("Mouse", 200m, 2)
    },
    "SAVE10"
);

var invoice = GenerateInvoice(order);
Console.WriteLine($"Total: {invoice.Total:C}");  // Total: Y5,439.60
```

### 模式 5：partial application 与柯里化

```csharp
// 柯里化 (Currying) — 将多参数函数转换为单参数函数链
static Func<int, Func<int, int>> Add()
    => a => b => a + b;

var curriedAdd = Add();
var add5 = curriedAdd(5);
Console.WriteLine(add5(3));   // 8
Console.WriteLine(add5(10));  // 15

// partial application — 固定部分参数
static Func<decimal, decimal, decimal, decimal> CalculateTotal
    = (price, taxRate, discount) => price * (1 + taxRate) - discount;

Func<decimal, decimal> With13PercentTax(Func<decimal, decimal, decimal, decimal> fn)
    => (price, discount) => fn(price, 0.13m, discount);

var calculateWithTax = With13PercentTax(CalculateTotal);
Console.WriteLine(calculateWithTax(100m, 10m));  // 103
```

### 常见踩坑提醒

1. **过度抽象**：不要为了函数式而把简单逻辑拆成十几个超级泛化的小函数
2. **性能陷阱**：过长的 LINQ 链可能导致多次迭代；必要时使用 `.ToList()`/`.ToArray()` 固话中间结果
3. **与 OOP 的边界**：函数式风格最适合**数据处理层**和**业务规则层**；I/O 和 UI 层保持实用主义
4. **错误处理**：Result 模式是好工具，但不要用它替代所有异常；对程序错误（非预期状态）仍该抛异常

---

## Level 5: 下一步

> 目标：知道应该往什么方向深入，以及如何继续实践。

### 进阶主题

#### 1. Monad 与函数式设计模式

- **Option Monad**：处理可空值
- **Result/Either Monad**：处理错误（避免异常流）
- **Reader Monad**：依赖注入的函数式版本
- **State Monad**：在不可变世界中管理状态
- **Writer Monad**：日志收集

**推荐材料**：
- 《Functional Programming in C#》 第 10-15 章
- Language-Ext 库源码阅读

#### 2. 不可变数据结构

- `System.Collections.Immutable` 命名空间（ImmutableArray, ImmutableDictionary 等）
- 持久化数据结构 (Persistent Data Structures) 的原理
- 平衡树和哈希 trie 在不可变集合中的应用

#### 3. 惰性求值 (Lazy Evaluation)

- `IEnumerable<T>` 的延迟执行机制
- `Lazy<T>` 的使用场景
- 无限序列（与 `yield return` 结合）

#### 4. 类型级编程

- 使用泛型约束实现类型安全的函数式模式
- 高种类多态 (Higher-Kinded Polymorphism) — C# 没有原生支持，但 Language-Ext 用变通方式实现了
- 判别联合类型 (Discriminated Union) 在 C# 中的实现技巧

#### 5. 响应式与并发

- **Rx.NET (Reactive Extensions)**：将事件流视为可观察的数据管道
  - `IObservable<T>` / `IObserver<T>`
  - 组合子：`.Select()`, `.Where()`, `.Merge()`, `.CombineLatest()`
- **PLINQ**：为 LINQ 查询自动并行化（`.AsParallel()`）
- **Channel<T>**：.NET 中的生产者-消费者模式

### 推荐迷你项目

> 练习是最好的老师。选择其中一个项目，用纯函数式风格完成。

**项目 1: 表达式计算器**
- 输入字符串如 `"3 + 4 * 2 / (1 - 5)^2"`
- 解析为 AST（不可变树）
- 用纯函数求值
- 使用模式匹配实现操作符分派

**项目 2: 购物车定价引擎**
- 多规则折扣（满减、会员折扣、组合促销）
- 每种折扣是一个纯函数：`Cart -> Cart`
- 组合所有规则为一个管道
- 用 Result 处理促销代码无效等错误情况

**项目 3: 股票行情监控**
- 用 Rx.NET 订阅实时价格流
- 用纯函数计算移动平均线、RSI 等指标
- 用不可变消息表示每个价格更新
- 用组合模式构建交易信号

### 持续学习资源

| 方向 | 资源 |
|------|------|
| C# 函数式深度 | 《Functional Programming in C#》— Enrico Buonanno |
| 通用函数式思维 | 《Functional Thinking》— Neal Ford |
| 类别论入门 | 《Category Theory for Programmers》— Bartosz Milewski |
| C# 函数式库 | Language-Ext (GitHub: louthy/language-ext) |
| 实践项目 | 用 F# 写一个小项目（感受纯函数式语言的体验） |
| 论文式阅读 | "Why Functional Programming Matters" — John Hughes |

### 如何寻求帮助

- Stack Overflow 标签：`c#` + `functional-programming`
- C# Discord 的 #functional-programming 频道
- Language-Ext 的 GitHub Discussions
- 阅读开源项目代码：Petabridge、Akka.NET 中的函数式部分

---

*学习函数式编程不只是学一种编码风格，更是培养一种新的思维方式。静下心来多读写代码，享受那种 "输入 -> 变换 -> 输出" 的清晰感。*
