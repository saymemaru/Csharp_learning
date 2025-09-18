using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class 条件判断类
    {
        public void SwitchCase()
        {
            string sku = "01-MN-L";

            string[] product = sku.Split('-');

            string type = "";
            string color = "";
            string size = "";

            switch (product[0])
            {
                case "01":
                    type = "Sweat shirt";
                    break;
                case "02":
                    type = "T-Shirt";
                    break;
                case "03":
                    type = "Sweat pants";
                    break;
                default:
                    type = "Other";
                    break;
            }

            switch (product[1])
            {
                case "BL":
                    color = "Black";
                    break;
                case "MN":
                    color = "Maroon";
                    break;
                default:
                    color = "White";
                    break;
            }

            switch (product[2])
            {
                case "S":
                    size = "Small";
                    break;
                case "M":
                    size = "Medium";
                    break;
                case "L":
                    size = "Large";
                    break;
                default:
                    size = "One Size Fits All";
                    break;
            }
            Console.WriteLine($"Product: {size} {color} {type}");
        }

        public string 辨别性别1(string 传入字符串)
        {
            string personString;
            switch (传入字符串)
            {
                case "男":
                    Console.WriteLine("男性");
                    return personString = "男性";
                case "女":
                    Console.WriteLine("女性");
                    return personString = "女性";
                default:
                    Console.WriteLine("不明生物");
                    return personString = "不明生物";
            }

        }
        public string 辨别性别2(string person)
        {
            var personString = person switch
            {
                "男" => "男性",
                "女" => "女性",
                "直升飞机" => "man",
                _ => "不明生物"
            };

            return personString;
        }

        //A ? b:c
        // 判断A bool，左true右false
        public void 三元条件运算符号()
        {
            Random dice = new Random();
            int roll6 = dice.Next(1, 6);
            Console.WriteLine(roll6 > 3 ? "tails" : "head");
        }

        public void FizzBuzz()
        {
            for (int i = 1; i <= 100; i++) 
            {
                if ((i % 3 == 0) && (i % 5 == 0))
                    Console.WriteLine($"{i} - FizzBuzz");
                else if (i % 3 == 0)
                    Console.WriteLine($"{i} - Fizz");
                else if (i % 5 == 0)
                    Console.WriteLine($"{i} - Buzz");
                else
                    Console.WriteLine($"{i}");
            }
        }
    }
}
