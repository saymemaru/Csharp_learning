using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PanelTest.CustomControl
{
    class CustomTextBox : TextBox
    {
        public CornerRadius TextBoxCornerRadius
        {
            get { return (CornerRadius)GetValue(TextBoxCornerRadiusProperty); }
            set { SetValue(TextBoxCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxCornerRadiusProperty =
            DependencyProperty.Register("TextBoxCornerRadius", typeof(CornerRadius), typeof(CustomTextBox));


    }
}
