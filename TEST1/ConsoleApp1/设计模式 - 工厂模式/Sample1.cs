using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式__工厂模式
{
    internal class SimpleFactory
    {
        public static Product CreateProduct(string type)
        {
            Product product = null;
            switch (type)
            {
                case "A":
                    product = new ConcreteProductA();
                    break;
                case "B":
                    product = new ConcreteProductB();
                    break;
                default:
                    throw new ArgumentException("Invalid type");
            }
            return product;
        }
    }

    abstract class Product
    {
        public abstract void Use();
    }

    class ConcreteProductA : Product
    {
        public override void Use()
        {
            Console.WriteLine("Using Product A");
        }
    }

    class ConcreteProductB : Product
    {
        public override void Use()
        {
            Console.WriteLine("Using Product B");
        }
    }
}
