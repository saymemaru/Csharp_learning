using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___外观模式
{
    //外观类,为子系统接口提供一个统一的高层接口
    class Facade
    {
        SubSystemA subA;
        SubSystemB subB;
        SubSystemC subC;
        public Facade()
        {
            subA = new SubSystemA();
            subB = new SubSystemB();
            subC = new SubSystemC();
        }

        //排列组合方法组
        public void MethodA()
        {
            Console.WriteLine("方法组A:");
            subA.MethodA();
            subB.MethodB();
        }
        public void MethodB()
        {
            Console.WriteLine("方法组B:");
            subB.MethodB();
            subC.MethodC();
        }
    }

    //子系统类
    class SubSystemA
    {
        public void MethodA()
        {
            Console.WriteLine("SubSystemA.MethodA");
        }
    }

    class SubSystemB
    {
        public void MethodB()
        {
            Console.WriteLine("SubSystemB.MethodB");
        }
    }
    class SubSystemC
    {
        public void MethodC()
        {
            Console.WriteLine("SubSystemC.MethodC");
        }
    }
}
