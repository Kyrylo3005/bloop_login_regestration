using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace bloop_login_regestration.Converters
{
    // Left or Right depending on bool (false => Left, true => Right)
    public class BoolToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            return HorizontalAlignment.Left;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Opposite of BoolToAlignmentConverter (false => Right, true => Left)
    public class BoolToOppositeAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            return HorizontalAlignment.Left;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Returns a Brush depending on owner/other user
    public class BoolToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush OtherBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#11141D"));
        private static readonly SolidColorBrush OwnBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#282C36"));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? OwnBrush : OtherBrush;
            return OtherBrush;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // For avatar ellipse alignment (same as BoolToAlignmentConverter)
    public class BoolToEllipseAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            return HorizontalAlignment.Left;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
