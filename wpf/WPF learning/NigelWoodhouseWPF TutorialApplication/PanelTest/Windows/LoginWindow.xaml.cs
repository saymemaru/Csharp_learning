using PanelTest.ViewModels;
using System.Windows;

namespace PanelTest.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel _loginViewModel;

        public LoginWindow()
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel(this);
            this.DataContext = _loginViewModel;
        }
    }
}
