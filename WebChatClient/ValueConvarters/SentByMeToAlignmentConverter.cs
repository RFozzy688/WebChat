using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает логическое значение, если сообщение было отправлено мной, 
    /// и выравнивается по правому краю. Если отправлено не мной, выравнивается по левому краю
    /// </summary>
    public class SentByMeToAlignmentConverter : BaseValueConverter<SentByMeToAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            else
                return (bool)value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
