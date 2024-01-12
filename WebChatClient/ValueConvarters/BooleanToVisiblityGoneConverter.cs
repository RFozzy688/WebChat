using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Преобразователь, который принимает логическое значение и возвращает <see cref="Visibility"/>
    /// где ложь<see cref="Visibility.Collapsed"/>
    /// </summary>
    public class BooleanToVisiblityGoneConverter : BaseValueConverter<BooleanToVisiblityGoneConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            else
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
