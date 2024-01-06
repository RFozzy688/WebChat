using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает дату и преобразует ее в удобное для 
    /// пользователя время чтения сообщения
    /// </summary>
    public class TimeToReadTimeConverter : BaseValueConverter<TimeToReadTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Получите время, прошедшее в
            var time = (DateTimeOffset)value;

            // Если оно не прочитано...
            if (time == DateTimeOffset.MinValue)
            {
                return string.Empty;
            }

            // Если это сегодня...
            if (time.Date == DateTimeOffset.UtcNow.Date)
            {
                return $"Read {time.ToLocalTime().ToString("HH:mm")}";
            }

            // В противном случае верните полную дату
            return $"Read {time.ToLocalTime().ToString("HH:mm, dd MMM yyyy")}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
