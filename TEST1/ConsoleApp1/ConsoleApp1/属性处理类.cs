using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleApp1
{
    internal class 属性处理类 //
    {

        public void 人类属性展示方法(人类 人类者)//需要方法实例化，属性处理类 人类属性展示 = new 属性处理类()
        {
            Console.WriteLine("姓名:" + 人类者.姓名 + " 性别:" + 人类者.性别 + " 身高:" + 人类者.身高 + "cm 体重:" + 人类者.体重 + "kg");
        }
        public static double 人类BMI计算方法(人类 人类者)//static静态方法 类.方法()直接引用
        {
            double meterheight = (double)人类者.身高 / 100;//int转double输出小数
            double BMI = 人类者.体重 / meterheight / meterheight;
            return BMI;
        }
    }

    

}
