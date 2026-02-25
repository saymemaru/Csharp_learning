using ManageSystem.EF6;
using ManageSystem.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem
{
    public partial class AddMenuForm : Form
    {
        private MenuPage CurrentMenuPage { get; set; }
        public AddMenuForm(MenuPage menuPage)
        {
            InitializeComponent();
            CurrentMenuPage = menuPage;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string menuText = textBox1.Text;
            string menuImage = textBox2.Text;
            string menuPage = textBox3.Text;
            if (string.IsNullOrEmpty(menuText))
            {
                MessageBox.Show("请填写菜单名称");
                return;
            }
            if (string.IsNullOrEmpty(menuImage))
            {
                MessageBox.Show("请填写菜单图片");
                return;
            }
            if (string.IsNullOrEmpty(menuPage))
            {
                MessageBox.Show("请填写菜单页面");
                return;
            }
            using (AppDbContext db = new())
            {
                MenuTModel menuTModel = new MenuTModel();
                menuTModel.MenuText = menuText;
                menuTModel.MenuImage = menuImage;
                menuTModel.MenuPage = menuPage;
                db.Menus.Add(menuTModel);

                db.SaveChanges();
            }

            //重新加载Menus数据，关闭页面
            CurrentMenuPage.LoadDBMenus();
            MessageBox.Show("添加完成"); 
            this.Close();

            
        }
        
    }
}
