using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HRIS.Source.Common
{
    public class StatusColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Free")
            {
                return Brushes.Green;
            } else if (value.ToString() == "Teaching")
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#e32213");
                return brush;
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#33959e");
                return brush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
