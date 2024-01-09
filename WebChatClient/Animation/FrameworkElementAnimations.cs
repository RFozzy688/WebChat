using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Помощники для анимации элементов фреймворка определенными способами.
    /// </summary>
    public static class FrameworkElementAnimations
    {
        /// <summary>
        /// Сдвигает элемент справа
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из правой анимации
            sb.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Сдвигает элемент влево
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из правой анимации
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Сдвигает элемент справа
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из правой анимации
            sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Сдвигает элемент вправо
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из правой анимации
            sb.AddSlideToRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Сдвигает элемент снизу
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Сохранять ли элемент на той же высоте во время анимации</param>
        /// <param name="height">Высота анимации для анимации. Если не указано, используется высота элементов.</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int height = 0)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд анимации снизу 
            sb.AddSlideFromBottom(seconds, height == 0 ? element.ActualHeight : height, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождать, пока закончится
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Сдвигает элемент вниз
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент на той же высоте во время анимации</param>
        /// <param name="height">Высота анимации для анимации. Если не указано, используется высота элементов.</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int height = 0)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд в нижнюю анимацию
            sb.AddSlideToBottom(seconds, height == 0 ? element.ActualHeight : height, keepMargin: keepMargin);

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой
            element.Visibility = Visibility.Visible;

            // Подождать, пока закончится
            await Task.Delay((int)(seconds * 1000));
        }

    }
}
