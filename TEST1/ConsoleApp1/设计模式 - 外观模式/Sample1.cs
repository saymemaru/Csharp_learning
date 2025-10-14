using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___外观模式
{
    //外观
    //目标：为子系统接口提供一个统一的高层接口
    //结构：外观类和若干子系统
    //子系统提供基本方法
    //外观类引用所有子系统，并提供组合方法

    //排列组合
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

        //组合方法
        public void MethodsA()
        {
            Console.WriteLine("方法组A:");
            subA.MethodA();
            subB.MethodB();
        }
        public void MethodsB()
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
