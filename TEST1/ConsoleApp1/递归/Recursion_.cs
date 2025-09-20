using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 递归
{
    public static class Recursion_
    {
        //递归函数：一个函数直接或间接调用自身的编程技术
        //递归函数通常包含两个主要部分：基准情况（base case）和递归情况（recursive case）。
        //基准情况是递归终止的条件，递归情况是函数调用自身以解决更小的子问题。

        public static int times = 0;
        public static void 汉诺塔(int n, char A, char B, char C)
        {

            if (n == 1)
            {
                move(A, C);
            }
            else
            {
                汉诺塔(n - 1, A, C, B);//将n-1个盘子由A经过C移动到B
                move(A, C);             //执行最大盘子n移动
                汉诺塔(n - 1, B, A, C);//剩下的n-1盘子，由B经过A移动到C
            }
        }

        private static void move(char A, char C)
        {
            Console.WriteLine("move:" + A + "--->" + C);
            times++;
        }

        public static int 累加(int n)
        {
            if (n == 1)
            {
                return n;
            }
            else
            {
                int sum = n + 累加(n - 1);
                return sum;
            }
        }
    }
}
