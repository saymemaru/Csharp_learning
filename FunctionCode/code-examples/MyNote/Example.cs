using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote
{
    public class Example
    {
        // --防止副作用--
        // 1. 不修改共享状态
        // 2. 不修改输入参数
        // 3. 不抛出异常
        // 4. 不执行 I/O 操作

        // --纯函数--
        // 相同输入，返回相同结果
        public decimal CalcDiscount(decimal amout, decimal discountRate)
        {
            return amout * (1 - discountRate);
        }

        // --不可变类型--
        // Immutable
        public ImmutableList<int> AddNumToList(ImmutableList<int> inputList)
        {
            //返回一个新列表
            return inputList.Add(1);
        }

        // --C#中的函数--
        // 1. method
        static bool IsMultipleOf(int a, int b)
        {
            return a % b == 0;
        }

        // 2. lambda expression
        readonly Func<int, int> addOne = x => x + 1;

        // 3. delegate
        Func<int, int, bool> predicateVar = IsMultipleOf;


        // --组合函数--
        static Func<int, int> add = x => x + 1;
        static Func<int, int> mutiply = x => x * 2;
        Func<int, int> composed = x => add(mutiply(x));
        // composed(3) => 7
        // 3 * 2 + 1 = 7

        // --函数组合--
        // 将简单函数组合成复杂函数
        // 1. 可读性
        // 2. 可复用性
        // 3. 可测试性
        // 4. 可维护性

        // --高阶函数--
        // 将函数作为参数或返回值
        // 1. 抽象化
        // 2. 复用性
        // 3. 灵活性

        // --闭包--
        // 函数+环境变量
        // 1. 数据隐藏
        // 2. 工厂函数
        // 3. 回调函数

        // --Currying--
        // 

    }
}
