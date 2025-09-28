using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___模板方法
{
    abstract class AbstractClass
    {
        //模板方法
        public void TemplateMethod()
        {
            //调用基本方法
            RequiredOperation1();
            RequiredOperation2();
            Console.WriteLine("");
        }

        //待实现的基本方法
        public abstract void RequiredOperation1();
        public abstract void RequiredOperation2();
    }

    //具体的类,实现基本方法
    class ConcreteClassA : AbstractClass
    {
        public override void RequiredOperation1()
        {
            Console.WriteLine("AbstractClassA.RequiredOperation1");
        }
        public override void RequiredOperation2()
        {
            Console.WriteLine("AbstractClassA.RequiredOperation2");
        }
    }

    class ConcreteClassB : AbstractClass
    {
        public override void RequiredOperation1()
        {
            Console.WriteLine("AbstractClassB.RequiredOperation1");
        }
        public override void RequiredOperation2()
        {
            Console.WriteLine("AbstractClassB.RequiredOperation2");
        }
    }


}