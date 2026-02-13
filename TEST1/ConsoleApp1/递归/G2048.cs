using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static G2048.Game2048;

namespace G2048

{
    public static class Game2048
    {
        //通过数组索引修改引用类型指向的堆中对象，可以省去返回引用类型引用
        //如Array.Sort()

        //把0移动到末尾
        public static void MoveZeroToEnd(int[] nums)
        {
            int zeroCount = 0;
            for (int i = 0; i < nums.Length - zeroCount; i++)
            {
                if (nums[i] == 0)
                {
                    zeroCount++;
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        nums[j-1] = nums[j];
                    }
                    nums[^1] = 0;
                }
            }
            //return nums;

            //将非0元素放入新数组
            /*int[] newNums = new int[nums.Length];
            int count = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] != 0)
                {
                    newNums[count++] = nums[j];
                }
            }
            newNums.CopyTo(nums);*/
        }

        //合并相邻相同的数，并把0移动到末尾
        public static void CombineSameNumbers(int[] nums)
        {
            MoveZeroToEnd(nums);
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == 0)
                    continue;
                if (nums[i] == nums[i + 1])
                {
                    nums[i] += nums[i + 1];
                    nums[i + 1] = 0;
                }
            }
            MoveZeroToEnd(nums);
        }

        //上移
        public static void MoveUp(int[,] nums2d)
        {
            int[] nums = new int[nums2d.GetLength(1)];
            for (int x = 0; x < nums2d.GetLength(0); x++)
            {
                for(int y = 0; y < nums2d.GetLength(1); y++)
                {
                    nums[y] = nums2d[x,y];
                }

                CombineSameNumbers(nums);

                for (int y = 0; y < nums2d.GetLength(1); y++)
                {
                    nums2d[x,y] = nums[y];
                }
            }
        }

        //下移，按倒序获取1d数组合并后，再倒序返回到2d数组
        public static void MoveDown(int[,] nums2d)
        {
            int[] nums = new int[nums2d.GetLength(1)];
            for (int x = 0; x < nums2d.GetLength(0); x++)
            {
                int i = -1;
                for (int y = nums2d.GetLength(1) - 1; y >= 0; y--)
                {
                    i++;
                    nums[i] = nums2d[x, y];
                }
                CombineSameNumbers(nums);

                for (int y = 0; y < nums2d.GetLength(1); y++)
                {
                    nums2d[x, y] = nums[i];
                    i--;
                }
            }
        }

        //左移
        public static void MoveLeft(int[,] nums2d)
        {
            int[] nums = new int[nums2d.GetLength(0)];
            for (int y = 0; y < nums2d.GetLength(1); y++)
            {
                for (int x = 0; x < nums2d.GetLength(0); x++)
                {
                    nums[x] = nums2d[x, y];
                }
                CombineSameNumbers(nums);

                for (int x = 0; x < nums2d.GetLength(0); x++)
                {
                    nums2d[x, y] = nums[x];
                }
            }
           
        }

      /*  public static int[,] MoveRight(int[,] nums2d)
        {
            int[] nums = new int[nums2d.GetLength(0)];
            for (int y = 0; y < nums2d.GetLength(1); y++)
            {
                int i = -1;
                for (int x = nums2d.GetLength(0) - 1; x <= 0 ; x--)
                {
                    i++;
                    nums[i] = nums2d[x, y];
                }
                nums = CombineSameNumbers(nums);

                for (int x = 0; x < nums2d.GetLength(0); x++)
                {
                    nums2d[x, y] = nums[i];
                    i--;
                }
            }
            return nums2d;
        }
*/
        public static void MoveRight(int[,] nums2d)
        {
            int[] nums = new int[nums2d.GetLength(0)];
            for (int y = 0; y < nums2d.GetLength(1); y++)
            {
                int i = -1;
                for (int x = nums2d.GetLength(1) - 1; x >= 0; x--)
                {
                    i++;
                    nums[i] = nums2d[x, y];
                }
                CombineSameNumbers(nums);

                for (int x = 0; x < nums2d.GetLength(1); x++)
                {
                    nums2d[x, y] = nums[i];
                    i--;
                }
            }
          
        }

        public enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public static void Move(MoveDirection direction, int[,] map)
        {
            switch(direction)
            {
                case (MoveDirection.Up):
                   MoveUp(map);
                   break;
                case (MoveDirection.Down):
                    MoveDown(map);
                    break;
                case (MoveDirection.Left):
                    MoveLeft(map);
                    break;
                case (MoveDirection.Right):
                    MoveRight(map);
                    break;

            }
                
        }

        public static void PrintDoubleArray(Array array)
        {
            Console.WriteLine();
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    Console.Write(array.GetValue(x,y) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

}
