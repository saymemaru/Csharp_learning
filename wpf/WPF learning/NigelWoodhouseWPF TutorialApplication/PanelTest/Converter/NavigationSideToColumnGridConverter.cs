using PanelTest.ViewModels;
using System.Globalization;
using System.Windows.Data;

namespace PanelTest.Converter
{
    public class NavigationSideToColumnGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NavigationSide navigationSide = (NavigationSide)value;
            return navigationSide == NavigationSide.Left 
                ? 0//Grid.Column 的值
                : 2;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
