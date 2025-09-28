using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_代理模式
{
    //方法接口
    interface IBehavior
    {
        void StealMoney();
        void EarnMoney();
        void SpendMoney();
    }

    //具体方法类
    class Master : IBehavior
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

    //代理类，带有具体方法类的引用
    class Proxy : IBehavior
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
