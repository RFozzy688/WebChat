using System;
using System.Diagnostics;
using System.Globalization;

using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Преобразует ApplPage в фактическое представление/страницу.
    /// </summary>
    public class IoCConverter : BaseValueConverter<IoCConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter) 
            {
                // поиск соответствующей стрвницы
                case nameof(AppVM):
                    return IoC.Get<AppVM>();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
