using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //可以实现自定义集合，并使其可枚举
    //
    //IEnumerable接口 IEnumerator接口
    //为非泛型集合提供了一种简单的迭代方式。
    //它只包含一个方法GetEnumerator()，该方法返回一个IEnumerator对象，
    //这个对象允许你通过公开Current属性以及MoveNext和Reset方法来遍历集合。
    //
    //当你实现了IEnumerable接口，你也必须实现IEnumerator接口。
    //这样做的好处是可以使用foreach语法来遍历集合

    //定义一个Person类
    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }
    }
    //自定义集合数据类型（就如同list，dic，array）
    //实现IEnumerable接口
    public class People : IEnumerable
    {
        private Person[] _people;

        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }
    //实现IEnumerator接口
    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;
        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
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
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };
            List<Person> peopleList = new List<Person>
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };
            People people = new People(peopleArray);

            //便可以使用foreach语法来遍历自定义集合people（建立迭代器的目的！简单的遍历）
            foreach (Person p in people)
                Console.WriteLine(p.firstName + " " + p.lastName);

            //何时使用 IEnumerable<T> (一次性)
            // - 当你想返回一个只读的、可遍历的集合时
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
}



