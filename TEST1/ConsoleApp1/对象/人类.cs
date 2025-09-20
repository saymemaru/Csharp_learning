using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 对象和类
{

    //构造函数
    public class 人类 : IDisposable
    {

        // C#垃圾回收机制，会自动管理内存，释放不再使用的对象
        // 你也可以实现IDisposable接口，手动释放资源
        // 析构函数可以让你在对象被垃圾回收之前执行一些清理操作

        // 资源释放标志
        private bool _disposed = false;
        // 实现IDisposable接口
        // 销毁对象
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) 
            {
                if (disposing)
                {
                    // 释放托管资源
                }
                // 释放非托管资源
                _disposed = true;
            }
        }

        public int 身高;
        public int 体重;
        public int 年龄;
        public string 性别 = "未知";
        public string 姓名 = "无名氏";
        public string[] 三种联系方式 = new string[3];

        //property
        private string birthday;
        public string Birthday
        {
            get 
            {
                if(birthday == null)
                {
                    return birthday = "未知日期";
                }
                return birthday;
            }
            set 
            { 
                if (value == "1984")
                {
                    birthday = "不存在";
                }
                birthday = value;
            }
        }
        


        /*
        -构造函数（函数名与类名相同）,默认无参数
        -创建新对象后会立即执行,同一类可定义多个构造函数
        -是特殊的成员函数
        */
        public 人类() : this("小王",10)
        {}
        //参数化构造函数
        public 人类(string 姓名)
        {
            this.姓名 = 姓名;
        }
        public 人类(string _姓名, int _年龄)
        {
            姓名 = _姓名;
            年龄 = _年龄;
        }

        //成员函数
        public void 你好() 
        {
            Console.WriteLine($"{姓名}向你问好");
        }

        public void 显示生日()
        {
            Console.WriteLine($"{Birthday}");
        }

      
    }
}
