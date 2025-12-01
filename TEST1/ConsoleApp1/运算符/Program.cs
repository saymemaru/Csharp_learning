using static System.Runtime.InteropServices.JavaScript.JSType;

//++ --
int a = 1;
Console.WriteLine(a++);//后加，执行之后返回值
Console.WriteLine(a);
int b = 1;
Console.WriteLine(++b);//前加，执行之前返回值
Console.WriteLine(b);

//+=、-=快捷运算符，不会改变类型

Console.WriteLine(Test.BitSummation1(123));
Console.WriteLine(Test.BitSummation2(333));

//隐式转换 小到大自动转换
byte b1 = 100; //0110 0100 1个字节
int c1 = b1; // 4个字节

//显式转换 大到小强制转换
int c2 = 100; 
byte b2 = (byte)c2; //byte范围0-255

static class Test
{
    //数字各位求和，字符串
    public static int BitSummation1(int num)
    {
        int sum = 0;
        string numString = num.ToString();
        for (int i = 0; i < numString.Length; i++)
        {
            sum += int.Parse(numString[i].ToString());
        }
        return sum;
    }

    //数字各位求和，取余
    public static int BitSummation2(int num)
    {
        int sum = 0;
        //转字符串
        int digitCount = num.ToString().Length;
        //对数
        digitCount = (int)Math.Log10(num) + 1;
        
        for (int i = 0; i < digitCount; i++)
        {
            if (i != digitCount - 1)
                sum += num / (int)Math.Pow(10, i) % 10;
            else
                //最大一位
                sum += num / (int)Math.Pow(10, i);
        }
        return sum;
    }
}

