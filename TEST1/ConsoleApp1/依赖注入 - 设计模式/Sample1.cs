using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
namespace 设计模式_依赖注入
{
    //有多种Glass可供Person选择
    internal interface IGlass
    {
        void SeeThroughGlass();
    }

    internal class SunGlass : IGlass
    {
        void IGlass.SeeThroughGlass()
        {
            Console.WriteLine("through the SunGlass");
        }
    }

    internal class ColorGlass : IGlass
    {
        void IGlass.SeeThroughGlass()
        {
            Console.WriteLine("through the ColorGlass");
        }
    }

    internal class Person
    {
        //有一个眼镜接口等待注入
        private readonly IGlass _glass;

        //非依赖注入，紧耦合
        //private Glass _glass = new Glass();

        //依赖注入，"拿"眼镜，不管什么眼镜
        public Person(IGlass glass)
        {
            _glass = glass;
        }

        public void Watch()
        {
            Console.WriteLine("Person: Staring the distant place ");
            _glass.SeeThroughGlass();
        }
    }
}



