using System;
using System.ComponentModel;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Базовое прикрепленное свойство для замены стандартного прикрепленного свойства WPF.
    /// </summary>
    /// <typeparam name="Parent">Родительский класс будет присоединенным свойством</typeparam>
    /// <typeparam name="Property">Тип прикрепленного свойства</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : new()
    {
        /// <summary>
        /// Запускается при изменении значения
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// Запускается, когда значение изменяется, даже если значение одинаковое
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        /// <summary>
        /// Единственный экземпляр нашего родительского класса
        /// </summary>
        public static Parent Instance { get; private set; } = new Parent();

        /// <summary>
        /// Прикрепленное свойство для этого класса
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", 
            typeof(Property), 
            typeof(BaseAttachedProperty<Parent, Property>), 
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// Событие обратного вызова при изменении <смотри cref="ValueProperty"/>.
        /// </summary>
        /// <param name="d">Элемент пользовательского интерфейса, свойство которого изменено</param>
        /// <param name="e">Аргументы для события</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Вызов родительского метода
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(d, e);

            // Вызов прослушивателей событий
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(d, e);
        }

        /// <summary>
        /// Событие обратного вызова при изменении <смотри cref="ValueProperty"/>, даже если 
        /// это то же самое значение.
        /// </summary>
        /// <param name="d">Элемент пользовательского интерфейса, свойство которого изменено</param>
        /// <param name="e">Аргументы для события</param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            // Вызов родительского метода
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(d, value);

            // Вызов прослушивателей событий
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(d, value);

            // Вернуть значение
            return value;
        }

        /// <summary>
        /// Получает присоединенное свойство
        /// </summary>
        /// <param name="d">Элемент, из которого нужно получить свойство</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Устанавливает прикрепленное свойство
        /// </summary>
        /// <param name="d">Элемент, из которого нужно получить свойство</param>
        /// <param name="value">Значение, которое нужно установить для свойства</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        /// <summary>
        /// Метод, который вызывается при изменении любого прикрепленного свойства этого типа.
        /// </summary>
        /// <param name="sender">Элемент пользовательского интерфейса, для которого было изменено это свойство.</param>
        /// <param name="e">Аргументы для этого события</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// Метод, который вызывается при изменении любого прикрепленного свойства этого типа, 
        /// даже если значение остается тем же.
        /// </summary>
        /// <param name="sender">Элемент пользовательского интерфейса, для которого было изменено это свойство.</param>
        /// <param name="e">Аргументы для этого события</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }
    }
}
