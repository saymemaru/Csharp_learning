using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 迭代_协程
{
    public interface IEatable
    {
        float HungryValue { get; }
    }
    public class Human : IEatable
    {
        public float HungryValue { get => _value; }
        private float _value;
        public Human(float value) 
        {
            _value = value;
        }
    }

    public class Dinner : IEnumerable
    {
        IEatable[] Eatable { get; set; }

        public Dinner(IEatable[] pArray)
        {
            Eatable = new IEatable[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                Eatable[i] = pArray[i];
            }
        }

        // 等同于 PeopleEnumerator

        // 调用 MoveNext 时执行
        // 执行到 yield 时离开当前方法
        // 再次调用 MoveNext 时继续执行
        public IEnumerator GetEnumerator()
        {
            // yield之前的代码被分配到MoveNext方法中
            // return之后的代码被分配到Current属性中
            for (int i = 0; i < Eatable.Length;i++)
            {
                yield return Eatable[i].HungryValue;
            }
        }
    }
}
