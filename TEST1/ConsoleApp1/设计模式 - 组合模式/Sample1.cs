using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_组合模式
{
    //component对象声明接口，实现所有类的默认行为，带有管理子部件的方法
    abstract class Component
    {
        protected string name;
        public Component(string name)
        {
            this.name = name;
        }
        public abstract void Add(Component a);
        public abstract void Remove(Component a);
        public abstract void Display(int depth);

    }

    class LeafNode : Component
    {
        public LeafNode(string name) : base(name)
        {
        }

        public override void Add(Component a)
        {
            Console.WriteLine("叶节点不存在子节点");
        }
        public override void Remove(Component a)
        {
            Console.WriteLine("叶节点不存在子节点");
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-',depth) + name);
        }
    }

    class CompositeNode : Component
    {
        private List<Component> childrenNode = new List<Component>();
        public CompositeNode(string name) : base(name)
        {
        }
        public override void Add(Component a)
        {
            childrenNode.Add(a);
        }
        public override void Remove(Component a)
        {
            childrenNode.Remove(a);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-',depth) + name);
            //遍历子节点
            foreach (Component component in childrenNode)
            {
                component.Display(depth + 2);
            }
        }

    }
}
