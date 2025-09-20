using System;
using System.Collections.Generic;
using System.Linq;

namespace 枚举
{
    public class Enum_
    {
        //枚举是一种特殊的数据类型，它允许你定义一组有（字符串）命名的常量，这些常量通常表示相关的值集合
        //默认底层类型是int，可以指定其他整数类型如byte, sbyte, short, ushort, uint, long, ulong
        //默认第一个枚举成员的值为0，后续成员的值依次递增1
        
        public enum Color
        {
            Red,
            Green,
            Blue,
            Yellow
        }

        public enum 日期
        {
            星期一 = 1,
            星期二 = 2,
            星期三 = 3,
            //可以不指定，默认下一个值加1
            星期四,
            星期五,
            //可以间断开
            星期天 = 7
        }
        //字符串
        public static void 枚举日期(string 星期几)
        {
            日期 星期;
            if (Enum.TryParse(星期几, out 星期))
            {   
                Console.WriteLine($"{星期}/" + $"{(int)星期}");
            }   
        }
        //枚举
        public static void 枚举日期(日期 星期)
        {
           Console.WriteLine($"{星期}/" + $"{(int)星期}");
        }
        //数字
        public static void 枚举日期(int dayNumber)
        {
            if(Enum.IsDefined(typeof(日期), dayNumber))
                Console.WriteLine($"{(日期)dayNumber}/" + $"{(int)(日期)dayNumber}");
        }

        public static void Test()
        {
            Color myColor = Color.Red;
            switch (myColor)
            {
                case Color.Red:
                    Console.WriteLine("The color is Red");
                    break;
                case Color.Green:
                    Console.WriteLine("The color is Green");
                    break;
                case Color.Blue:
                    Console.WriteLine("The color is Blue");
                    break;
                case Color.Yellow:
                    Console.WriteLine("The color is Yellow");
                    break;
                default:
                    Console.WriteLine("Unknown color");
                    break;
            }
            //枚举转字符串
            string colorName = myColor.ToString();
            Console.WriteLine($"Color name: {colorName}");
            //字符串转枚举
            if (Enum.TryParse<Color>("Green", out Color parsedColor))
            {
                Console.WriteLine($"Parsed color: {parsedColor}");
            }
            else
            {
                Console.WriteLine("Failed to parse color");
            }
            //获取所有枚举值
            var allColors = Enum.GetValues(typeof(Color)).Cast<Color>();
            Console.WriteLine("All colors: " + string.Join(", ", allColors));
        }
    }
}
