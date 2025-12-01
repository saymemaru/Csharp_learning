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

        //1 - 2 + 3 - 4 + 5 ...x 求奇正偶负的和
        public static int 奇正偶负和(int num)
        {
            if (num == 1)
            {
                return 1;
            }
            if (num % 2 == 0)
            {
                return 奇正偶负和(num - 1) - num;
            }
            else
            {
                return 奇正偶负和(num - 1) + num;
            }
        }

        //先执行一次do中语句，再while判断
        public static void DoWhile()
        {
            int a = 0;
            do
            {
                a++;
            }
            while (a < 5);
        }

        //break跳出while循环
        public static void WhileBreak()
        {
            int a = 0;
           
            while (true)
            {
                a++;
                if (a > 5)
                    break;
            }

        }

        //冒泡排序，每轮比较中遇到一个较小值就交换
        public static int[] 冒泡排序(int[] nums)
        {
            for (int currentIndex = 0; currentIndex < nums.Length - 1; currentIndex++)
            {
                for (int otherIndex = currentIndex + 1; otherIndex < nums.Length; otherIndex++)
                {
                    if (nums[otherIndex] < nums[currentIndex])
                    {
                        (nums[otherIndex], nums[currentIndex]) = (nums[currentIndex], nums[otherIndex]);
                    }
                }    
            }
            return nums;
        }

        //选择排序，每轮比较中只交换最小值
        public static int[] 选择排序(int[] nums)
        {
            for (int currentIndex = 0; currentIndex < nums.Length - 1; currentIndex++)
            {
                //记录最小数序号
                int minIndex = currentIndex;
                for (int otherIndex = currentIndex + 1; otherIndex < nums.Length; otherIndex++)
                {
                    if (nums[otherIndex] < nums[minIndex])
                    {
                        minIndex = otherIndex;
                    }
                }
                //防止交互相同数字的位置
                if (nums[currentIndex] != nums[minIndex])
                {
                    (nums[minIndex], nums[currentIndex]) = (nums[currentIndex], nums[minIndex]);
                }
            }
            return nums;
        }

        //选择排序，每轮比较中只交换最小值
        public static bool 检查重复元素(int[] nums)
        {
            for (int currentIndex = 0; currentIndex < nums.Length - 1; currentIndex++)
            {
                for (int otherIndex = currentIndex + 1; otherIndex < nums.Length; otherIndex++)
                {
                    if (nums[currentIndex] == nums[otherIndex])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
