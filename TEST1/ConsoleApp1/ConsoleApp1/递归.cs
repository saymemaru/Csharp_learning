using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class 递归
    {
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
