using ManageSystem.EF6;
using ManageSystem.Model;
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
    public partial class AddPersonForm : Form
    {
        private PersonPage CurrentPersonPage { get; set; }
        public AddPersonForm(PersonPage personPage)
        {
            InitializeComponent();
            CurrentPersonPage = personPage;

            /*List<Object> roles = new ()
            {
                new {Id = 1, Name = "新兵" },
                new {Id = 2, Name = "老兵" },
                new {Id = 3, Name = "管理员" },
            };*/

            // 枚举获取值
            comboBoxRole.DataSource = Enum.GetValues(typeof(RoleEnum));
            /*comboBoxRole.DisplayMember = "Name";
            comboBoxRole.ValueMember = "Id";*/
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string Id = textBox1.Text;
            string name = textBox2.Text;
            string gender = textBox3.Text;
            string address = textBox4.Text;
            //int roledId = int.Parse(comboBoxRole.SelectedValue.ToString());
            int roleId = (int)(RoleEnum)comboBoxRole.SelectedValue;
            //string roleName = roleId.ToString();
            if (string.IsNullOrEmpty(Id))
            {
                MessageBox.Show("请填写Id");
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请填写姓名");
                return;
            }
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("请填写性别");
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("请填写地址");
                return;
            }
            using (AppDbContext db = new())
            {
                PersonTModel personTModel = new PersonTModel();
                personTModel.PersonId = int.Parse(Id);
                personTModel.Name = name;
                personTModel.Gender = gender;
                personTModel.Address = address;
                personTModel.RoleId = roleId;
                db.Persons.Add(personTModel);

                db.SaveChanges();
            }

            //重新加载Menus数据，关闭页面
            CurrentPersonPage.LoadDBPersons();
            MessageBox.Show("添加完成"); 
            this.Close();

            
        }
        
    }
}
