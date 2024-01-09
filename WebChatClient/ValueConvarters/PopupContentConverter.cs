using System;
using System.Globalization;

namespace WebChatClient
{
    /// <summary>
    /// Конвертер, который принимает <see cref="BaseViewModel"/> и возвращает конкретный элемент 
    /// управления пользовательского интерфейса это должно быть привязано к этому типу модели 
    /// представления
    /// </summary>
    public class PopupContentConverter : BaseValueConverter<PopupContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChatAttachmentPopupMenuVM basePopup)
                return new VerticalMenu { DataContext = basePopup.Content };

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
