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
    public partial class EditPersonForm : Form
    {
        private PersonPage CurrentPersonPage { get; set; }
        private int PersonId { get; set; }

        public EditPersonForm(PersonPage personPage, string Id)
        {
            InitializeComponent();
            CurrentPersonPage = personPage;

            PersonId = int.Parse(Id);

            using(AppDbContext db =new())
            {
                //FirstOrDefault返回Menus中第一个满足 e.Id == menuId 条件的元素
                PersonTModel? personTModel = db.Persons.FirstOrDefault(e => e.PersonId == PersonId);
                textBox1.Text = personTModel.Name;
                textBox2.Text = personTModel.Gender;
                textBox3.Text = personTModel.Address;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string personName = textBox1.Text;
            string personGender = textBox2.Text;
            string personAddress = textBox3.Text;
            if (string.IsNullOrEmpty(personName))
            {
                MessageBox.Show("请填写姓名");
                return;
            }
            if (string.IsNullOrEmpty(personGender))
            {
                MessageBox.Show("请填写性别");
                return;
            }
            if (string.IsNullOrEmpty(personAddress))
            {
                MessageBox.Show("请填写地址");
                return;
            }
            using (AppDbContext db = new())
            {
                PersonTModel? personTModel = db.Persons.FirstOrDefault(m => m.PersonId == PersonId);

                personTModel.Name = personName;
                personTModel.Gender = personGender;
                personTModel.Address = personAddress;

                db.SaveChanges();
            }

            //重新加载Menus数据，关闭页面
            CurrentPersonPage.LoadDBPersons();
            MessageBox.Show("保存完成"); 
            this.Close();

        } 
    }
}
