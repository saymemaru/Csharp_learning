using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式__策略模式
{
    //策略类，存储特定的方法
    public abstract class Strategy
    {
        public abstract string name { get; }
        public abstract void Execute();
    }

    public  class StrategyHello : Strategy
    {
        public override string name => "Hello";

        public override void Execute()
        {
            Console.WriteLine("Hello");
        }
    }

    public  class StrategyWorld : Strategy
    {
        public override string name => "World";
        public override void Execute()
        {
            Console.WriteLine("World!");
        }
    }

    //上下文，存储策略的引用，及执行策略的方法
    public class Context
    {
        private Strategy _strategy;

        //传入字符串而不是对象，不暴露具体策略
        public Context(string? type)
        {
            switch(type)
            {   
                case "Hello":
                    _strategy = new StrategyHello();
                    break;
                case "World":
                    _strategy = new StrategyWorld();
                    break;
                default:
                    throw new ArgumentException("Invalid type");
            }
        }

        public void SetStrategy(Strategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteStrategy()
        {
            _strategy.Execute();
        }
    }

}
