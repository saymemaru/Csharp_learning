using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式_状态模式
{
    abstract class State
    {
        public abstract void ChangeState(Context context);
    }

    class SleepState : State
    {
        public override void ChangeState(Context context)
        {
            if (context.Hour >= 0 && context.Hour <= 7)
            {
                Console.WriteLine("Sleeping");
            }
            else
            {
                context.SetState(new WonderState());
                context.Request();
            }
        }
    }

    class WonderState : State
    {
        public override void ChangeState(Context context)
        {
            if (context.Hour <= 24 && context.Hour >= 8 && !context.IsBusy
                || context.Hour <= 24 && context.Hour >= 19)
            {
                Console.WriteLine("Wondering");
            }
            else if (context.Hour <= 18 && context.Hour >= 8 && context.IsBusy)
            {
                context.SetState(new WorkState());
                context.Request();
            }
            else if (context.Hour >= 0 && context.Hour <= 7)
            {
                context.SetState(new SleepState());
                context.Request();
            }
        }
    }

    class WorkState : State
    {
        public override void ChangeState(Context context)
        {
            if (context.Hour >= 8 && context.Hour <= 18 && context.IsBusy)
            {
                Console.WriteLine("Working");
            }
            else if (context.Hour <= 24 && context.Hour >= 8 && !context.IsBusy
                || context.Hour <= 24 && context.Hour >= 19)
            {
                context.SetState(new WonderState());
                context.Request();
            }
        }
    }


    //上下文
    //包含State引用, 
    class Context
    {
        public int Hour { get; set; }
        public bool IsBusy { get; set; } = false;

        private State _state;
        public Context(State state)
        {
            _state = state;
        }
        public State State
        {
            get { return _state; }
            set { _state = value;}
        }
        public void Request()
        {
            _state.ChangeState(this);
        }
        public void SetState(State state)
        {
            Console.WriteLine("过去状态: " + _state.GetType().Name);
            _state = state;
        }
    }
}
