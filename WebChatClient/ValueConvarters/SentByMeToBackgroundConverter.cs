using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает логическое значение, если сообщение было отправлено мной, и возвращает
    /// правильный цвет фона
    /// </summary>
    public class SentByMeToBackgroundConverter : BaseValueConverter<SentByMeToBackgroundConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Application.Current.FindResource("VeryLightBlueBrush") : Application.Current.FindResource("VeryLightBrush");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
