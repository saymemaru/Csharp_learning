using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_原型模式
{
    //通用
    //抽象原型类
    abstract class Prototype
    {
        public Prototype(string id) 
        { 
            this.id = id;
        }

        private string id;
        public string Id { get; }

        //克隆方法
        public abstract Prototype Clone();
    }
    //具体原型类
    class ConcretePrototypeA : Prototype
    {
        //: base(id) 调用基类的构造函数,并传递参数id
        public ConcretePrototypeA(string id) : base(id)
        {
        }

        public override Prototype Clone()
        {
            //浅拷贝：创建当前对象的浅表副本
            //创建一个新对象，其非静态字段与目标对象具有相同的值
            //值类型逐位复制
            //类类型复制引用
            //该副本与原始对象引用同一对象
            return (Prototype)this.MemberwiseClone();
        }
    }


    //.NET提供了ICloneable接口来实现克隆功能
    //实现ICloneable的Clone方法即
    abstract class Person : ICloneable
    {
        protected string phoneNum = "null";
        protected HomeAddress homeAddress;
        public string PhoneNum 
        { 
            get { return phoneNum; }
            set { phoneNum = value; }
        }
        public Person(string phoneNum)
        {
            this.PhoneNum = phoneNum;
            this.homeAddress = new HomeAddress();
        }
        protected Person(HomeAddress homeAddress)
        {
            
        }

        public void SetHomeAddress(string country, string city)
        {
            this.homeAddress.Country = country;
            this.homeAddress.City = city;
        }
        public void SetPhoneNum(string phoneNum)
        {
            this.PhoneNum = phoneNum;
        }
        public void ShowInfo()
        {
            Console.WriteLine($"PhoneNum: {this.phoneNum}");
            Console.WriteLine($"Country:{homeAddress.Country}, City:{homeAddress.City}");
        }

        public abstract object Clone();
    }

    //浅拷贝
    class PersonA : Person
    {
        public PersonA(string phoneNum) : base(phoneNum)
        {
        }

        public override object Clone()
        {
            //会clone值类型的值,和引用类型的引用
            return this.MemberwiseClone();
        }
    }

    //深拷贝
    //引用类型成员也实现了ICloneable接口
    class PersonB : Person
    {
        public PersonB(string phoneNum): base(phoneNum)
        {
        }

        //将需要深拷贝的引用类型作为参数传入私有构造函数
        private PersonB(HomeAddress homeAddress) :base(homeAddress)
        {
            //把引用对象的变量指向复制的新对象，而不是原有的被引用的对象
            this.homeAddress = (HomeAddress)homeAddress.Clone();
        }

        //重写Clone方法,让新对象调用私有构造函数(Clone引用对象)
        public override object Clone()
        {
            PersonB obj = new PersonB(this.homeAddress);
            //值类型需要逐条复制?
            obj.PhoneNum = this.phoneNum;
            return obj;
        }
    }

    //引用类型成员
    class HomeAddress : ICloneable
    {
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string country = "None";

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string city = "None";
        //实现ICloneable接口

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


    //简化版深拷贝
    class Cat : ICloneable
    {
        private HomeAddress HomeAddress;
        public Cat()
        {
            this.HomeAddress = new HomeAddress();
        }
        private Cat(HomeAddress homeAddress)
        {
            this.HomeAddress = (HomeAddress)homeAddress.Clone();
        }

        public void SetHomeAddress(string country, string city)
        {
            this.HomeAddress.Country = country;
            this.HomeAddress.City = city;
        }
        public void ShowInfo()
        {
            Console.WriteLine($"Country:{HomeAddress.Country}, City:{HomeAddress.City}");
        }

        public object Clone()
        {
            Cat obj = new Cat(this.HomeAddress);
            return obj;
        }
    }



 
}
