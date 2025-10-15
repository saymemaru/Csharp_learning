using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_单例模式
{
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
    //静态初始化，该单例在加载时就实例化，提前占用系统资源
    public sealed class Singleton2
    {
        private static readonly Singleton2 instance = new Singleton2();
        private Singleton2() { }

        public static Singleton2 Instance = instance;

        public void Hello()
        {
            Console.WriteLine("我是饿汉");
        }

    }
}
