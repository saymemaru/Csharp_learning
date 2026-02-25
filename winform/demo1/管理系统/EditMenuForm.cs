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
    // 找到对应Id的数据进行修改
    public partial class EditMenuForm : Form
    {
        private MenuPage CurrentMenuPage { get; set; }
        private int MenuId { get; set; }

        public EditMenuForm(MenuPage menuPage, string Id)
        {
            InitializeComponent();
            CurrentMenuPage = menuPage;

            MenuId = int.Parse(Id);

            using(AppDbContext db =new())
            {
                //FirstOrDefault返回Menus中第一个满足 e.Id == menuId 条件的元素
                MenuTModel? menuTModel = db.Menus.FirstOrDefault(e => e.Id == MenuId);
                textBox1.Text = menuTModel.MenuText;
                textBox2.Text = menuTModel.MenuImage;
                textBox3.Text = menuTModel.MenuPage;
            }
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
                MenuTModel? menuTModel = db.Menus.FirstOrDefault(m => m.Id == MenuId);

                menuTModel.MenuText = menuText;
                menuTModel.MenuImage = menuImage;
                menuTModel.MenuPage = menuPage;

                db.SaveChanges();
            }

            //重新加载Menus数据，关闭页面
            CurrentMenuPage.LoadDBMenus();
            MessageBox.Show("保存完成"); 
            this.Close();

        } 
    }
}
