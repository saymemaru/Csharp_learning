using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_代理模式
{
    //两个类(主类和代理类)继承同一个的接口
    //代理类有主类的引用，通过实现接口方法调用主类的方法

    //实现，调用代理类的方法，实际上是调用主类的方法的效果
    interface IBehavior
    {
        void StealMoney();
        void EarnMoney();
        void SpendMoney();
    }

    class Master: IBehavior
    {
        public Target target { get; private set; }

        public int moneyChangeAbility { get; set; } = 10;
        public Master(Target target, int moneyChangeAbility)
        {
            this.target = target;
            this.moneyChangeAbility = moneyChangeAbility;
        }

        public void EarnMoney()
        {
            target.money = target.money - moneyChangeAbility;
        }
        public void SpendMoney()
        {
            target.money = target.money + moneyChangeAbility;
        }
        public void StealMoney()
        {
            target.money = target.money - moneyChangeAbility * 2;
        }
    }
    
    class Proxy: IBehavior
    {
        public Master master { get; set; }
        public Proxy(Master master)//可用工厂模式创建Master对象
        {
            this.master = master;
        }
        public void EarnMoney()
        {
            master.EarnMoney();
        }
        public void SpendMoney()
        {
            master.SpendMoney();
        }
        public void StealMoney()
        {
            master.StealMoney();
        }
    }

    class Target
    {
        public int money { get; set; } = 100;
        public Target(int money)
        {
            this.money = money;
        }
    }

}
