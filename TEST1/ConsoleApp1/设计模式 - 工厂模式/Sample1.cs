using System;
using System.Reflection;
using System.Configuration;

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
    //缺点: 添加产品种类需要修改简单工厂
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
    // 定义一个创建对象的接口，让具体工厂类决定实例化哪一个类
    //抽象工厂接口

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


    //抽象工厂
    //提供一个创建一系列相关或相互依赖对象的接口，而无需指定它们具体的类
    //抽象工厂接口, 抽象产品接口
    
    interface IAnimal
    {
        void Use();
    }
    class Cat : IAnimal
    {
        public void Use()
        {
            Console.WriteLine("Cat is used");
        }
    }
    class Dog : IAnimal
    {
        public void Use()
        {
            Console.WriteLine("Dog is used");
        }
    }
    interface IFood
    {
        void Eat();
    }
    class CatFood : IFood
    {
        public void Eat()
        {
            Console.WriteLine("Cat food is eaten");
        }
    }
    class DogFood : IFood
    {
        public void Eat()
        {
            Console.WriteLine("Dog food is eaten");
        }
    }
    interface IAbstractFactory
    {
        IAnimal CreateAnimal();
        IFood CreateFood();
    }
    class DogFactory : IAbstractFactory
    {
        public IAnimal CreateAnimal()
        {
            return new Dog();
        }
        public IFood CreateFood()
        {
            return new DogFood();
        }
    }
    class CatFactory : IAbstractFactory
    {
        public IAnimal CreateAnimal()
        {
            return new Cat();
        }
        public IFood CreateFood()
        {
            return new CatFood();
        }
    }

    //反射+配置文件实现的简单工厂
    class Factory
    {
        //程序集名称
        private static readonly string assemblyName = "设计模式 - 工厂模式";
        //类所在命名空间
        private static readonly string namespaceName = "设计模式_工厂模式";
        //引用System.Configuration
        //using System.Configuration;
        //通过App.config配置文件获取类标识
        private static readonly string raceName = ConfigurationManager.AppSettings["raceName"];

        public static IAnimal CreateAnimal()
        {
            //字符串拼好类
            //命名空间.类名
            string className = namespaceName + "." + raceName;
            return (IAnimal)Assembly.Load(assemblyName).CreateInstance(className);
        }
        public static IFood CreateFood()
        {
            string className = namespaceName + "." + raceName + "Food";
            return (IFood)Assembly.Load(assemblyName).CreateInstance(className);
        }

        //...添加更多的创建方法...
    }
}
