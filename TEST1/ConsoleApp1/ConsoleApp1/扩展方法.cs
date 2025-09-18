using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class 扩展方法
    {
        //扩展方法，只能用在静态类中
        //第一个参数指定要扩展的类型，必须使用 this 关键字修饰
        //text.ToPascalCase()调用
        //你已经用过了例如：num.ToString()
        public static string ToPascalCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        //集合到字符串扩展方法
        public static string ToElementsString<T>(this IEnumerable<T> list, string separator = ",")
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                var itemString = item.ToString();
                if(!string.IsNullOrEmpty(itemString))
                    sb.Append(itemString).Append(separator);
            }
            return sb.ToString();
        }

        //获取Color枚举的字符串扩展方法
        public static string GetColor(this Color color)
        {
            return color.ToString();
        }
    }
}
