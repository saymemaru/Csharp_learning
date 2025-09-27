using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_工厂模式
{
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

    //简单工厂
    //输入标识符，返回对应的产品实例
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


    //方法工厂

    interface IFactory
    {
        Product CreateProduct();
    }

    class ProductAFactory : IFactory
    {
        public Product CreateProduct()
        {
            return new ConcreteProductA();
        }
    }

    class ProductBFactory : IFactory
    {
        public Product CreateProduct()
        {
            return new ConcreteProductB();
        }
    }
}
