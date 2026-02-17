using ManageSystem.EF6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem.Pages
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
            using(AppDbContext db = new() )
            {
                List<MenuTModel> menuTModels = db.Menus.ToList();
                dataGridView1.DataSource = menuTModels;
            }
        }
    }
}
