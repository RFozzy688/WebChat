
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WebChatClient
{
    /// <summary>
    /// Преобразователь базовых значений, позволяющий напрямую использовать XAML.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        /// <summary>
        /// Cтатический экземпляр этого преобразователя значений.
        /// </summary>
        private static T _converter = null;

        /// <summary>
        /// Предоставляет статический экземпляр преобразователя значений.
        /// </summary>
        /// <param name="serviceProvider">Поставщик услуг</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        /// <summary>
        /// Метод преобразования одного типа в другой
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        /// <summary>
        /// Метод преобразования значения обратно в его исходный тип.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
