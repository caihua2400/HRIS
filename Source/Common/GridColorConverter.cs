using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HRIS.Source.Common
{
    class GridColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "0")
            {
                return Brushes.White;
            }
            else if (value.ToString() == "1") // Consultation
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#33959e");
                return brush;
            }
            else if (value.ToString() == "2") // Teaching e32213
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#e32213");
                return brush;
            }
            else
            {
                return Brushes.DarkGray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
