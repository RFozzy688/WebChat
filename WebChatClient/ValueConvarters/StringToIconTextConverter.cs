using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает строку и возращает текстовую иконку
    /// </summary>
    public class StringToIconTextConverter : BaseValueConverter<StringToIconTextConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "EmailIcon":
                    return "\uf0e0";
                case "KeyIcon":
                    return "\uf084";
                case "UserIcon":
                    return "\uf007";
                default:
                    return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
