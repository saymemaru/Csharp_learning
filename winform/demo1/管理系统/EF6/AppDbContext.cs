using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManageSystem.EF6
{
    internal class AppDbContext : DbContext
    {
        //数据库连接配置
        public AppDbContext() 
            : base("Server = LAPTOP-GREATWORD; Database=ManageSystemDB;Trusted_Connection=True")
        {
            
        }

        public DbSet<MenuTModel> Menus { get; set; }
    }
}
