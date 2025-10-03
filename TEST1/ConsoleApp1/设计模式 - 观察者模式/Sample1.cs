using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式___观察者模式
{
    //接口版
    interface Subject
    {
        void Attach(Player observer);
        void Detach(Player observer);
        void Notify();
        public string SubjectState { get; set; }
    }

    //委托版
    class Player : IDisposable
    {
        public static event EventHandler <EventArgs> OnSummoned;
        public static event EventHandler<EventArgs> OnDestroyed;
        public static event EventHandler <int> OnDamaged;
        
        public int Health { get; private set; } = 100;
        public string Name { get; private set; } = "null";

        public Player(string name)
        {
            Name = name;
            OnSummoned?.Invoke(this,EventArgs.Empty);
        }

        public void Damaged(int amount) 
        {
            Health -= amount;
            if(Health < 0)
            {
                Health = 0;
                OnDamaged?.Invoke(this, amount);
                OnDestroyed?.Invoke(this, EventArgs.Empty);
                Dispose();
            }
            else
            {
                OnDamaged?.Invoke(this, amount);
            }
            

        }



        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // 取消事件订阅
                    // 静态事件的取消需要在外部管理
                }
                disposed = true;
            }
        }
    }

    class HealthManager 
    {
        List<Player> players = new List<Player>();

        public void Player_OnDestroyed(object sender, EventArgs e)
        {
            Player player = (Player)sender;
            players.Remove(player);
            Console.WriteLine($"Player {player.Name} has been destroyed, {players.Count} player left now!");
        }
        public void Player_OnSummoned(object sender, EventArgs e)
        {
            Player player = (Player)sender;
            players.Add(player);
            Console.WriteLine($"Player {player.Name} has been summoned,{players.Count} player left now!");
        }
        public void Player_OnDamaged(object sender, int e) 
        {
            Player player = (Player)sender;
            Console.WriteLine($"Player {player.Name} took {e} damage and has {player.Health} health left!");

        }
    }

    



}
