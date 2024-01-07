using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает дату и преобразует ее в удобное для пользователя время.
    /// </summary>
    public class TimeToDisplayTimeConverter : BaseValueConverter<TimeToDisplayTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Получите время, прошедшее в
            var time = (DateTimeOffset)value;

            // Если это сегодня...
            if (time.Date == DateTimeOffset.UtcNow.Date)
            {
                return time.ToLocalTime().ToString("HH:mm");
            }

            // В противном случае вернём полную дату
            return time.ToLocalTime().ToString("HH:mm, dd MMM yyyy");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
