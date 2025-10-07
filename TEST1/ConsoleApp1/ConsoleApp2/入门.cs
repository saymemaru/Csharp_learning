using System;
namespace LeeCode
{
    public static class 入门
    {
        public static int[] SumOf1DArray(int[] nums)
        {
            int[] result = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                int sum = (i + 1) * (nums[0] + nums[i]) / 2;
                result[i] = sum;
                Console.Write(sum + " ");
            }
            Console.WriteLine(); 
            return result; 
        }

        public static int MaximumWealth(int[][] accounts)
        {
            int maxWealth = 0;
            for (int i = 0; i < accounts.Length; i++)
            {
                int[] value = accounts[i];
                int sum = 0;

                foreach (int item in value)
                {
                    sum += item;
                }
                if (sum > maxWealth)
                {
                    maxWealth = sum;
                }
            }
            Console.WriteLine(maxWealth);
            return maxWealth;
        }

        public static IList<string> FizzBuzz(int n)
        {
            //Array[i]对数组已分配空间直接赋值速度快
            //list.Add("a")时,若容量不够,会动态扩容,重新分配内存,速度慢
            string[] strings = new string[n];
            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    strings[ i - 1 ] = "FizzBuzz";
                }
                else if (i % 3 == 0)
                {
                    strings[i - 1] = "Fizz";
                }
                else if (i % 5 == 0)
                {
                    strings[i - 1] = "Buzz";
                }
                else
                {
                    strings[i - 1] = i.ToString();
                }
            }
            Console.WriteLine(string.Join(", ", strings));
            return strings;
        }

        //也可以用位运算,偶数右移一位,相当于除以2
        public static int NumberOfSteps(int num)
        {
            int steps = 0;
            while (num > 0)
            {
                if (num % 2 == 0)
                {
                    num /= 2;
                    steps++;
                }
                else
                {
                    num -= 1;
                    steps++;
                }
            }
            return steps;
        }

        /* public ListNode MiddleNode(ListNode head)
         {
             int count = 0;
             ListNode temp = head;

             while (temp != null)
             {
                 count++;
                 temp = temp.next;
             }

             for (int i = 0; i < count / 2; i++)
             {
                 head = head.next;
             }

             return head;
         }*/

        public static bool CanConstruct(string ransomNote, string magazine)
        {
            if(ransomNote.Length > magazine.Length) 
                return false;
            int[] counts = new int[26];
            foreach (char c in magazine)
            {
                counts[c - 'a']++;
            }
            foreach (char c in ransomNote)
            {
                if (--counts[c - 'a'] < 0)
                    return false;
            }
            Console.WriteLine(ransomNote + "_" + magazine);
            return true;
        }
        public static bool IsSubsequence(string ransomNote, string magazine)
        {
            int i = 0, j = 0;
            while (i < ransomNote.Length && j < magazine.Length)
            {
                if (ransomNote[i] == magazine[j])
                    i++;
                j++;
            }
            return i == ransomNote.Length;
        }

        public static bool IsSubstring(string ransomNote, string magazine)
        {
            return magazine.Contains(ransomNote);
        }
    }
}
