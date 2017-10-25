using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace KoloryWPF
{
    public class ByteToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)(byte)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (byte)(double)value;
        }
    }

    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color kolor = (Color)value;
            return new SolidColorBrush(kolor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SkładoweRGBToSolidColorBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            byte r = (byte)values[0];
            byte g = (byte)values[1];
            byte b = (byte)values[2];
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            SolidColorBrush pędzel = value as SolidColorBrush;
            Color kolor = pędzel.Color;
            return new object[3] { kolor.R, kolor.G, kolor.B };
        }
    }
}
