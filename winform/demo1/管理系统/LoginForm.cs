#define DEBUG
using ManageSystem.EF6;
using ManageSystem.Pages;
using System;


namespace ManageSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string personId = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(personId))
            {
                MessageBox.Show("请填写用户Id");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请填写密码");
                return;
            }
            using (AppDbContext db = new())
            {
                PersonTModel? personTModel = db.Persons.FirstOrDefault(m => m.PersonId == int.Parse(personId) && m.Password == password);
                if (personTModel == null)
                {
                    MessageBox.Show("用户不存在 / 密码不正确");
                    return;
                }

                //当前登录用户信息
                UserState.Instance.CurrentLoginedUser = personTModel;
            }

            //重新加载Menus数据，关闭页面
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

#if DEBUG
        private void buttonDebug_Click(object sender, EventArgs e)
        {
            //重新加载Menus数据，关闭页面
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
#endif
    }
}
