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
        // Истинно, если это первый раз, когда значение обновляется.
        // Используется, чтобы убедиться, что мы запускаем логику хотя бы один раз во время первой загрузки.
        protected Dictionary<DependencyObject, bool> _alreadyLoaded = new Dictionary<DependencyObject, bool>();

        // Самое последнее значение, используемое, если мы изменили значение до первой загрузки.
        protected Dictionary<DependencyObject, bool> _firstLoadValue = new Dictionary<DependencyObject, bool>();

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Получить элемент фреймворка
            if (!(sender is FrameworkElement element))
                return;

            // Не запускать, если значение не изменится
            if ((bool)sender.GetValue(ValueProperty) == (bool)value && _alreadyLoaded.ContainsKey(sender))
                return;

            // При первой загрузке...
            if (!_alreadyLoaded.ContainsKey(sender))
            {
                // Пометить, что мы находимся на первой загрузке, но еще не завершили ее
                _alreadyLoaded[sender] = false;

                // Прежде чем решить, как анимировать, начните со скрытого изображения
                element.Visibility = Visibility.Hidden;

                // Создайте одно событие с возможностью самостоятельного отсоединения
                // для элементов события Loaded
                RoutedEventHandler onLoaded = null;
                onLoaded = async (ss, ee) =>
                {
                    // Отцепитесь от себя
                    element.Loaded -= onLoaded;

                    // Небольшая задержка после загрузки необходима для размещения некоторых элементов
                    // и их ширина/высота рассчитаны правильно
                    await Task.Delay(5);

                    // Сделайте желаемую анимацию
                    DoAnimation(element, _firstLoadValue.ContainsKey(sender) ? _firstLoadValue[sender] : (bool)value, true);

                    // Отметить, что мы завершили первую загрузку
                    _alreadyLoaded[sender] = true;
                };

                // Подключитесь к событию Loaded элемента
                element.Loaded += onLoaded;
            }
            // Если мы начали первую загрузку, но еще не запустили анимацию, обновите свойство
            else if (_alreadyLoaded[sender] == false)
                _firstLoadValue[sender] = (bool)value;
            else
                // Сделайте желаемую анимацию
                DoAnimation(element, (bool)value, false);
        }

        /// <summary>
        /// Метод анимации, который запускается при изменении значения.
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="value">Новое значение</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value, bool firstLoad) { }
    }

    /// <summary>
    /// Анимирует элемент фреймворка, сдвигая его слева на экране.
    /// и выдвигаемся влево при скрытии
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Left, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Анимирует элемент каркаса, скользящий вверх снизу на экране.
    /// и выдвижение вниз при скрытии
    /// </summary>
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Анимирует скольжение элемента каркаса снизу вверх при загрузке
    /// </summary>
    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            // Animate in
            await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, !value, !value ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Анимирует элемент каркаса, скользящий вверх снизу на экране.
    /// и выдвижение вниз при скрытии
    /// NOTE: Сохраняет Margin
    /// </summary>
    public class AnimateSlideInFromBottomMarginProperty : AnimateBaseProperty<AnimateSlideInFromBottomMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: true);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: true);
        }
    }

    /// <summary>
    /// Анимирует появление элемента структуры на экране.
    /// и исчезновение при скрытии
    /// </summary>
    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                // Animate in
                await element.FadeInAsync(firstLoad, firstLoad ? 0 : 0.3f);
            else
                // Animate out
                await element.FadeOutAsync(firstLoad ? 0 : 0.3f);
        }
    }
}
