using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___适配器模式
{
    class 英语壬
    {
        public virtual void SayHello()
        {
            Console.WriteLine("Hello");
        }
    }
    class 中文壬
    {
        public void 说你好()
        {
            Console.WriteLine("你好");
        }
    }

    class 翻译壬 : 英语壬
    {
        private 中文壬 adaptee = new 中文壬();
        public override void SayHello()
        {
            adaptee.说你好();
        }
    }
}
