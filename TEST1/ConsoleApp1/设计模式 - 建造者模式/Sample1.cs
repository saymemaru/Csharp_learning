using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___建造者模式
{
    //抽象建造者
    abstract class Builder
    {
        public Builder()
        {
        }
        public abstract void BuildFoundation();
        public abstract void BuildWalls();
        public abstract void BuildRoof();

        public abstract House GetHouse();
    }
    //产物
    class House
    {
        IList<string> parts = new List<string>();
        public void AddPart(string part)
        {
            parts.Add(part);
        }
        public void Show()
        {
            Console.WriteLine("House parts:");
            foreach (var part in parts)
            {
                Console.WriteLine(part);
            }
        }
    }
    //具体建造者
    class ConcreteBuilderA : Builder
    {
        private House house = new House();
        public ConcreteBuilderA() 
        { 
        }
        public override void BuildFoundation()
        {
            house.AddPart($"Building foundationA");
        }
        public override void BuildWalls()
        {
            house.AddPart($"Building wallsA");
        }
        public override void BuildRoof()
        {
            house.AddPart($"Building roofA");
        }

        public override House GetHouse()
        {
            return house;
        }
    }
    class ConcreteBuilderB : Builder
    {
        House house = new House();
        public ConcreteBuilderB() 
        {
        }
        public override void BuildFoundation()
        {
            house.AddPart($"Building foundationB");
        }
        public override void BuildWalls()
        {
            house.AddPart($"Building wallsB");
        }
        public override void BuildRoof()
        {
            house.AddPart($"Building roofB");
        }
        public override House GetHouse()
        {
            return house;
        }
    }

    //指挥者
    //引用建造者，按逻辑执行建造步骤
    class Director
    {
        //逻辑
        public void Construct(Builder builder)
        {
            builder.BuildFoundation();
            builder.BuildWalls();
            builder.BuildRoof();
        }
    }
}
