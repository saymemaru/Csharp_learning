using PanelTest.Data;
using PanelTest.ViewModels;
using System.Windows;

namespace PanelTest.Windows
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly MainViewModel _viewModel;
        public Window1(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += Window1_Loaded;
        }
        private async void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

    }
}
