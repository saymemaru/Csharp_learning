using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace 迭代_协程
{

    //迭代器模式
    //不管是什么数据集合类型，都可以通过一种方法获取其元素
    //IEnumerable、IEnumerator接口是.net中对迭代器模式的实现
    #region 自定义可迭代数据集合
    //可以实现自定义集合，并使其可枚举
    //
    //IEnumerable接口 IEnumerator接口
    //为非泛型集合提供了一种简单的迭代方式。
    //它只包含一个方法GetEnumerator()，该方法返回一个IEnumerator对象，
    //这个对象允许你通过公开Current属性以及MoveNext和Reset方法来遍历集合。
    //
    //当你实现了IEnumerable接口，你也必须实现IEnumerator接口。
    //
    //结果就是存在Enumerator的Enumerable对象可以使用foreach语法来遍历该对象

    //定义一个Person类
    public interface ISpeak
    {
        void Talk();
    }
    public class Person : ISpeak
    {
        public string firstName;
        public string lastName;

        public Person(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }

        public void Talk()
        {
            throw new NotImplementedException();
        }
    }
    //自定义集合数据类型（就如同list，dic，array）
    //数据集合实现IEnumerable接口
    public class People : IEnumerable
    {
        private ISpeak[] _animal;

        public People(ISpeak[] pArray)
        {
            _animal = new ISpeak[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                _animal[i] = pArray[i];
            }
        }

        /*IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }*/

        //获取迭代器
        public IEnumerator GetEnumerator()
        {
            return new PeopleEnumerator(_animal);
        }

    }
    //实现IEnumerator接口
    public class PeopleEnumerator : IEnumerator
    {
        public ISpeak[] _animal;
        int index = -1; //起始位置为空

        public PeopleEnumerator(ISpeak[] list)
        {
            _animal = list;
        }

        //实现MoveNext()，迭代器获取下一个位置的元素，可加入不同的逻辑规则
        public bool MoveNext()
        {
            index++;
            //如果位置索引小于集合长度，则返回
            return index < _animal.Length;
        }

        //迭代器索引回到初始位置
        public void Reset()
        {
            index = -1;
        }

        
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        //根据迭代器索引返回当前指向的对象
        public ISpeak Current
        {
            get
            {
                try
                {
                    return _animal[index];
                }
                //超出索引
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public class PeopleManager
    {
        public PeopleManager()
        {
            //数组
            ISpeak[] peopleArray = new ISpeak[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };
            //列表集合
            List<Person> peopleList = new List<Person>
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };
            //自定义集合
            People people = new People(peopleArray);

            //便可以使用foreach语法来遍历自定义集合people（建立迭代器的目的！简单的遍历）
            foreach (Person p in people)
                Console.WriteLine(p.firstName + " " + p.lastName);

            //何时使用 IEnumerable<T> 
            // - 当你想返回一个只读的、可遍历的(一次性)集合时
            // - 当你想延迟执行查询时
            // - 当你想提高内存效率时
            //
            //何时使用 List<T> 或 Array[] etc
            // - 当你需要频繁访问元素时
            // - 当你需要修改集合时
            // - 当你需要使用索引访问元素时
            // - 当你需要使用特定的集合操作时，如排序、搜索等
            // - 当你需要更复杂的数据结构时，如栈、队列、字典等

            //Cast<>()方法尝试将peopleList中的每个元素转换为Person类型
            //如果遇到无法转换的元素（如整数），则会抛出InvalidCastException异常
            IEnumerable<Person> _ = peopleList.Cast<Person>();
            //OfType<>()方法自动进行类型筛选，只转换那些已经是Person类型的元素
            IEnumerable<Person> p2 = people.OfType<Person>();
            //Append()方法用于向现有的IEnumerable<Person>集合添加一个新的Person对象
            IEnumerable<Person> p3 = peopleList.Append(new Person("aaa", "bbb"));
            //更多操作 etc..




        }

    }
    #endregion



}



