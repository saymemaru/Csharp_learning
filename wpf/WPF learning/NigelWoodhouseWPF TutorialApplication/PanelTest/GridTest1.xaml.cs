using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PanelTest
{
    /// <summary>
    /// GridTest1.xaml 的交互逻辑
    /// </summary>
    public partial class GridTest1 : Window
    {
        public GridTest1()
        {
            InitializeComponent();
        }

        private void BtnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.BtnMainWindow.Content = "Opened";
            Window? win = Activator.CreateInstance(typeof(MainWindow)) as Window;
            if (win != null)
                win.Show();

        }

        private void BtnMainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            Label? lab = Activator.CreateInstance(typeof(Label)) as Label;
            lab.Content = "enter btn!";
            lab.PointToScreen(e.GetPosition(this));

        }
    }
}
