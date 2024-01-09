using WebChatCore;
using System;
using System.Globalization;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает <see cref="MenuItemType"/> и возвращает <see cref="Visibility"/>
    /// на основе того, что данный параметр совпадает с типом пункта меню
    /// </summary>
    public class MenuItemTypeVisiblityConverter : BaseValueConverter<MenuItemTypeVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если у нас нет параметра, возвращаем невидимый
            if (parameter == null)
                return Visibility.Collapsed;

            // преобразовать строку параметра в перечисление
            if (!Enum.TryParse(parameter as string, out MenuItemType type))
                return Visibility.Collapsed;

            // виден, если параметр соответствует типу
            return (MenuItemType)value == type ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
