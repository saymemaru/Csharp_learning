using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 特征
{
    // 特性
    // 特性（Attribute）是用于向程序元素（如类、方法、属性等）添加额 外数据的一种机制

    //为类添加特性
    [Student("Math", 101)]
    public class Person
    {
        public string Name { get; set; }

        //为属性添加多个特性
        [Age("1926", "08", "18")]
        [Age("2026", "08", "18")]
        public int Age { get; set; }

        public string TrueBirthDay { get; set; }
        public string FalseBirthDay { get; set; }

        //为方法添加特性
        [Sleep(SleepTimeEnum.Medium)]
        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping.");
        }

        ////Obsolete特性，将元素标记为过时，并附加警告信息
        [Obsolete("Use Sleep() method instead.")]
        public void Sleep(int time)
        {
            Console.WriteLine($"{Name} is sleeping {time}.");
        }

        //根据Age的attribute,注入Person的TrueBirthDay和FalseBirthDay
        public void ShowBirthDayAttr()
        {
            var type = this.Age.GetType();
            var attrs = (AgeAttribute[])type.GetCustomAttributes(false);
            var trueBirthDay = attrs[0].Year + attrs[0].Month; 
            var falseBirthDay = attrs[1].Year + attrs[1].Month;
            this.TrueBirthDay = trueBirthDay;
            this.FalseBirthDay = falseBirthDay;
        }
    }
    public class StudentAttribute : Attribute
    {
        public string ClassName { get; set; }
        public int ClassNumber { get; set; }
        public StudentAttribute(string name, int age)
        {
            ClassName = name;
            ClassNumber = age;
        }
    }

    // AttributeUsage
    // AttributeTargets 用于限定向程序元素（如类、方法、属性等）添加元数据
    // AllowMultiple 表示是否允许在同一元素上有多个该特性
    // Inherited 表示该特性是否可以被派生类继承

    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field
        ,AllowMultiple = true, 
        Inherited = true)]
    public class AgeAttribute : Attribute
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public AgeAttribute(string year, string month, string day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SleepAttribute : Attribute
    {
        public int SleepHours { get; set; }
 
        public SleepAttribute(SleepTimeEnum time)
        {
            SleepHours = (int)time;
        }
    }

    public enum SleepTimeEnum
    {
        Short = 4,
        Medium = 8,
        Long = 12
    }
}
