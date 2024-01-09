using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Базовый класс для запуска любого метода анимации, когда для логического значения установлено значение true
    /// и обратная анимация, если установлено значение false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        // Флаг, указывающий, загружается ли это свойство в первый раз.
        public bool FirstLoad { get; set; } = true;

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Получить элемент фреймворка
            if (!(sender is FrameworkElement element))
                return;

            // Не запускать, если значение не изменится
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // При первой загрузке...
            if (FirstLoad)
            {
                // Создайте одно событие с возможностью самостоятельного отсоединения
                // для элементов Событие Loaded
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // Отцепитесь от себя
                    element.Loaded -= onLoaded;

                    // Сделайте желаемую анимацию
                    DoAnimation(element, (bool)value);

                    // Больше не в первой загрузке
                    FirstLoad = false;
                };

                // Подключитесь к событию Loaded элемента
                element.Loaded += onLoaded;
            }
            else
                // Сделайте желаемую анимацию
                DoAnimation(element, (bool)value);
        }

        /// <summary>
        /// Метод анимации, который запускается при изменении значения.
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="value">Новое значение</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    // Анимирует элемент фреймворка, сдвигая его слева на экране
    // и сдвигая влево при скрытии
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Анимировать в центр
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Анимация скрытия
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    // Анимирует элемент каркаса, скользящий снизу вверх на экране
    // и движение вниз при скрытии
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }
}
