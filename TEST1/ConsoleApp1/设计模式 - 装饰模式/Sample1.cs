using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式__装饰模式
{
    //抽象组件，便于拓展更多不同的具体组件
    abstract class Component
    {
        public abstract void Operation();
    }

    //具体的组件
    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("具体组件的操作");
        }
    }

    //抽象装饰器，便于拓展更多不同的具体装饰器
    abstract class Decorator : Component
    {
        protected Component _component;
        public void Decorate(Component component)
        {
            _component = component;
        }
        public override void Operation()
        {
            if (_component != null)
            {
                _component.Operation();
            }
        }
    }

    //具体的装饰器
    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();//base.Operation()的位置决定了添加行为的顺序
            AddedBehavior();
        }
        void AddedBehavior()
        {
            Console.WriteLine("具体装饰器A的操作");
        }
    }
    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
        }
        void AddedBehavior()
        {
            Console.WriteLine("具体装饰器B的操作");
        }
    }
}
