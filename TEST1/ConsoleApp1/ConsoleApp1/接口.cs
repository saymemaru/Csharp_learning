using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface I接口 { }

    //给接口添加限制
    interface IRepository<T> where T : I接口, new()
    {
        public T GetById(int id);
    }


    //接口名称常以I开头
    //接口方法是virtual,public的，没有实体，可以被重写
    //接口不能有field,constructor,destructor
    //接口可以多继承
    //接口是一种contract，告诉实现class必须做什么
    internal interface IAttacker
    {
        void Attack(IDefender target);
    }

    internal interface IDefender
    {
        void Defense(IAttacker target);
    }

    //接口组合
    internal interface IArmy : IAttacker, IDefender
    {

    }

    class Marine: IArmy
    {
        public string name { get;private set; }

        public Marine(string name)
        {
            this.name = name;
        }

        public void Attack(IDefender target)
        {
            Console.WriteLine("Marine attacks!");
            target.Defense(this);
        }
        public void Defense(IAttacker target)
        {
            Console.WriteLine("Marine defends!");
        }
    }

    class Zergling : IArmy
    {
        public void Attack(IDefender target)
        {
            Console.WriteLine("Zergling attacks!");
            target.Defense(this);
        }
        public void Defense(IAttacker target)
        {
            Console.WriteLine("Zergling defends!");
        }
    }

    public class BattleSimlator
    {
        //使用IArmy作为参数类型，可以接受任何实现了IArmy接口的对象，而非具体的Marine或Zergling对象
        internal static void Fright(IArmy attacker, IArmy defender)
        {
            attacker.Attack(defender);
        }
    }



}
