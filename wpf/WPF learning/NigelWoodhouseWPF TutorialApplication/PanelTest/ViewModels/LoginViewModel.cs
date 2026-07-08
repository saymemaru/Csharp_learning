using PanelTest.Command;
using PanelTest.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PanelTest.ViewModels
{
    class LoginViewModel : ValidationViewModelBase
    {
        private string? _password;
        private string? _userName;
        LoginWindow _loginWindow;

        public LoginViewModel(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
            LoginCommand = new RelayCommand(Login,CanLogin);
            UserName = "wpf";
            Password = "hi";
        }
        public RelayCommand LoginCommand { get; }
        public string? UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaiseProertyChanged();
                LoginCommand.RaiseCanExecuteChanged();

                if (string.IsNullOrEmpty(_userName))
                    AddError("UserName is required");
                else
                    ClearError();
                
            }
        }
        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaiseProertyChanged();
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        private void Login(object? parameter)
        {
            if (Password == "hi" && UserName == "wpf")
            {
                MessageBox.Show("登录成功");
                _loginWindow.Hide();
            }
            else
            {
                Password = "";
                UserName = "";
                MessageBox.Show("登录失败");
            }
        }
        private bool CanLogin(object? parameter) => !string.IsNullOrEmpty(UserName);

    }
}
