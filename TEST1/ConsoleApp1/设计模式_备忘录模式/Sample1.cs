using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_备忘录模式
{
    //发起类
    class Originator
    {
        private string state;
        public string State
        {
            get { return state; } 
            set {  state = value; }
        }

        public Memento CreateMemento()
        {
            return new Memento(state);
        }
        
        public void SetMemento(Memento menmento)
        {
            state = menmento.State;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"State = {state}");
        }
    }

    //备忘录类
    class Memento
    {
        private string state;
        public string State { get { return state; } }
        public Memento(string state)
        {
            this.state = state;
        }
    }


    //管理者类
    class Caretaker
    {
        private Memento memento;
        public Memento Memento
        {
            get { return memento; }
            set { memento = value; }
        }
    }
}
