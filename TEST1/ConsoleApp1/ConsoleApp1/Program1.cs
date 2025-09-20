using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using ClassLibrary1;

namespace ConsoleApp1 //引用不同命名空间内容时需要using xxx
{
    class Program1
    {
        static void Main(string[] args)
        {
           
            

           

            人类 小王 = new();
            小王.姓名 = "小王";
            小王.性别 = "男";
            小王.身高 = 175;
            小王.体重 = 60;
            小王.三种联系方式 = ["122413", "322144", "232334"];

            //类方法实例化
            属性处理类 属性处理实例 = new 属性处理类();
            属性处理实例.人类属性展示方法(小王);
            //static(类.方法)
            Console.WriteLine(string.Format("BMI={0:F2}", 属性处理类.人类BMI计算方法(小王)));
            //foreach、StarWith、Length方法示例
            数组和foreach示例();
            范型集合();
            //CaseSwitch
         

            

            for (int i = 0; i < 1; i++)  //for(变量；判断；计算）
            {
                inputstringjudgement();
            }
            Console.ReadKey();
        }
        static void inputstringjudgement()
        { //变量类型
            var n = 1;
            int m = 2;
            float k = 3;
            string name = "输入字符：";
            System.Console.WriteLine(name);

            bool 数字格式检查 = true;
            for (;数字格式检查;)
            {
                string 输入字符 = Console.ReadLine();

                bool 检查 = Regex.IsMatch(输入字符, @"^[0-9,-]+$");//正则表达式，数字字符串true
                if (检查)//判断检查 == && != || > <
                {
                    Console.WriteLine("输入了纯数字");
                    int 输出数字 = 0;

                    //try-catch 错误抛出
                    try
                    {
                        输出数字 = int.Parse(输入字符) + m;//类型转换（不改变原值"输入字符"）    
                        数字格式检查 = false;
                    }
                    catch//报错返回
                    {
                        Console.WriteLine("输入了数字，但格式错误，重新输入");
                        //结束程序，返回空
                    }

                    if (数字格式检查 == false)
                    {
                        if (输出数字 > 0)
                        { Console.WriteLine("正数"); }
                        else if (输出数字 == 0) { Console.WriteLine("零"); }
                        else { Console.WriteLine("负数"); }
                        Console.WriteLine("计算结果为: " + 输出数字.ToString());//字符串拼接
                    }
                }
                else
                {
                    string output = stringcombine(输入字符, name);//接收变量，传递变量
                    Console.WriteLine(output);
                    数字格式检查 = false;
                }
            }
        }

        //函数类型-返回值类型，传递参数类型
        static string stringcombine(string part1, string part2)
        {
            Console.WriteLine("没有输入纯数字");
            string 输出字符 = part2 + part1 + "\n";
            return 输出字符;
        }
        static void 范型集合()
        {
            List<string> 列表 = new List<string>();
            列表.Add("一");//列表[0]
            列表.Add("二");
            列表.Add("三");
            Console.WriteLine(列表.ToElementsString());
        }

        static void 数组和foreach示例()
        { 
            string[] 字符串数组示例 = new string[3];//new string[i]中，i表示数组创建时的数组大小
            字符串数组示例[0] = "干死你";
            字符串数组示例[1] = "干杯";
            字符串数组示例[2] = "你好";
            var 数组长度 = 字符串数组示例.Length;
            Console.Write("字符串数组示例中以\"干\"开头的字符串包括：");
            foreach (var 临时变量 in 字符串数组示例)
            {
                if (临时变量.StartsWith("干"))
                {
                    Console.Write($"{临时变量}，");
                }
            }
            Console.WriteLine($"\n字符串数组长度为：{数组长度}");

            int[] 整数数组示例 = new int[5];
            整数数组示例 = [12, 23, 54, 653, 1];
            int 求和 = 0;
            int 种类数 = 0;
            foreach (var 临时变量 in 整数数组示例)
            {
                求和 += 临时变量;
                种类数++;
                Console.WriteLine($"foreach找到了：{临时变量}");
            }
            Console.WriteLine($"整数数组求和 = {求和}，共有{种类数}个数");
        }

        //随机数生成
        static void Random示例()
        {
            Random dice = new Random();
            int roll1 = dice.Next(18);
            int roll2 = dice.Next(1, 6);
        }
        
                

    }
   
    
}

