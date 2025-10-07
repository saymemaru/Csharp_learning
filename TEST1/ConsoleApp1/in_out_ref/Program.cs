using 对象与类;

internal class Program
{
    private static void Main(string[] args)
    {
        //声明值类型时, 入栈
        //创建对象时, 对象引用入栈, 对象入堆

        //out, 必须在方法外声明, 并在方法内赋值
        //in, 只读参数, 传递引用但不可修改
        //ref, 必须已赋值, 引用目标在栈中的位置,可以在方法内修改对象内容
       
        
        人类 小王 = new();
        小王.身高 = 170;  
        Console.WriteLine(小王.身高);
        Function2(ref 小王);
        Console.WriteLine(小王.身高);

        int n = 0;
        Function4(n);
        Console.WriteLine(n);
        Function3(ref n);
        Console.WriteLine(n);
        


    }
    static void Function1(out int n)
    {
        n = 0;
        n++;
    }

    static void Function2(ref 人类 n)
    {
        //n指向对象的引用
        n.身高 = 200;
    }

    static void Function3(ref int n)
    {
        n++;
    }
    
    static void Function4(int n)
    {
        n++;
    }
}