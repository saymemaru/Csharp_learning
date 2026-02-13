using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_单例模式
{
    //参考 https://zhuanlan.zhihu.com/p/1954901902312576992

    /*
    1.优先使用 Lazy<T> - 最安全、最简洁的实现方式
    2.理解各种实现的适用场景 - 饿汉式适合轻量级对象，饱汉式适合重量级对象
    3.注意线程安全和性能平衡 - 根据实际需求选择合适方案
    4.考虑可测试性 - 在可能的情况下使用依赖注入
    */

    //单例模式是全局状态，要谨慎使用。只有在真正需要全局唯一实例时才使用它，
    //否则考虑使用依赖注入来管理对象生命周期。


    //懒汉单例，第一次引用时才初始化
    //需要双重锁定保证线程安全
    public sealed class Singleton1
    {
        private static Singleton1 instance = null;
        private static readonly object lockObj = new object(); //只读的进程辅助对象
        private Singleton1() { }
        
        public static Singleton1 Instance
        {
            get
            {
                //先判断单例是否存在，节约性能
                if(instance == null)
                {
                    //阻止其他线程进入
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton1();
                        }
                        return instance;
                    }
                }
                return instance;
            }
        }

        public void Hello()
        {
            Console.WriteLine("我是懒汉");
        }
    }

    //饿汉单例
    //静态只读字段方法是通过在类中定义一个静态只读字段来存储单例实例，这种方法简单且线程安全。
    //该单例在加载时就实例化，提前占用系统资源
    public sealed class Singleton2
    {
        private static readonly Singleton2 instance = new Singleton2();
        private Singleton2() { }

        public static Singleton2 Instance => instance;

        public void Hello()
        {
            Console.WriteLine("我是饿汉");
        }

    }

    //(现代) Lazy模式是C#特有的懒加载模式，它使用System.Lazy<T>类来延迟单例实例的创建，直到它被首次请求。
    public class SingleTon3
    {
        private static readonly Lazy<SingleTon3> instance = 
            new Lazy<SingleTon3>(() => new SingleTon3("param"));

        private readonly string data;
        private SingleTon3(string data)
        {
            this.data = data;
        }
        public static SingleTon3 Instance => instance.Value;

        public void Hello()
        {
            Console.WriteLine("我是Lazy");
        }
    }

   
   
}
