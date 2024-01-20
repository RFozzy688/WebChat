using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает логическое значение, если сообщение было отправлено мной, 
    /// острый угол по правому краю. Если отправлено не мной, острый угол по левому краю
    /// </summary>
    public class SentByMeCornerMarkConverter : BaseValueConverter<SentByMeCornerMarkConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? "10 10 0 10" : "10 10 10 0";
            else
                return (bool)value ? "10 10 10 0" : "10 10 0 10";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
