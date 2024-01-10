using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Помощники для анимации элементов фреймворка определенными способами.
    /// </summary>
    public static class FrameworkElementAnimations
    {
        /// <summary>
        /// Вдвигает элемент
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="direction">Направление слайда</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <param name="size">Ширина/высота анимации для анимации. Если не указано, используется размер элементов.</param>
        /// <param name="firstLoad">Указывает, является ли это первой загрузкой</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInAsync(this FrameworkElement element, 
            AnimationSlideInDirection direction, 
            bool firstLoad, 
            float seconds = 0.3f, 
            bool keepMargin = true, 
            int size = 0)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Скольжение в правильном направлении
            switch (direction)
            {
                // Добавить слайд из левой анимации
                case AnimationSlideInDirection.Left:
                    sb.AddSlideFromLeft(seconds, 
                        size == 0 ? element.ActualWidth : size, 
                        keepMargin: keepMargin);
                    break;
                // Добавить слайд из правой анимации
                case AnimationSlideInDirection.Right:
                    sb.AddSlideFromRight(seconds, 
                        size == 0 ? element.ActualWidth : size, 
                        keepMargin: keepMargin);
                    break;
                // Добавить слайд сверху анимации
                case AnimationSlideInDirection.Top:
                    sb.AddSlideFromTop(seconds, 
                        size == 0 ? element.ActualHeight : size, 
                        keepMargin: keepMargin);
                    break;
                // Добавить слайд снизу анимации
                case AnimationSlideInDirection.Bottom:
                    sb.AddSlideFromBottom(seconds, 
                        size == 0 ? element.ActualHeight : size, 
                        keepMargin: keepMargin);
                    break;
            }
            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой только в том случае, если мы анимируем или это первая загрузка
            if (seconds != 0 || firstLoad)
                element.Visibility = Visibility.Visible;

            // Ждать, пока закончится анимация
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Выдвигает элемент
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="direction">Направление слайда (это для обратного действия слайда, поэтому левое будет скользить влево)</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        /// <param name="size">Ширина/высота для анимации. Если не указано, используется размер элементов.</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutAsync(this FrameworkElement element, 
            AnimationSlideInDirection direction, 
            float seconds = 0.3f, 
            bool keepMargin = true, 
            int size = 0)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Скольжение в правильном направлении
            switch (direction)
            {
                // Добавить слайд в левую анимацию
                case AnimationSlideInDirection.Left:
                    sb.AddSlideToLeft(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Добавить слайд к правой анимации
                case AnimationSlideInDirection.Right:
                    sb.AddSlideToRight(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Добавить слайд в верхнюю анимацию
                case AnimationSlideInDirection.Top:
                    sb.AddSlideToTop(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                // Добавить слайд в нижнюю анимацию
                case AnimationSlideInDirection.Bottom:
                    sb.AddSlideToBottom(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
            }

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой, только если мы анимируем
            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));

            // Сделать элемент невидимым
            element.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Затухание элемента
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="firstLoad">Указывает, является ли это первой загрузкой</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, 
            bool firstLoad, 
            float seconds = 0.3f)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой только в том случае, если мы анимируем или это первая загрузка
            if (seconds != 0 || firstLoad)
                element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Затухание элемента
        /// </summary>
        /// <param name="element">Элемент для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="firstLoad">Указывает, является ли это первой загрузкой</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(element);

            // Сделать страницу видимой только в том случае, если мы анимируем или это первая загрузка.
            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            // Подождите, пока это закончится
            await Task.Delay((int)(seconds * 1000));

            // Полностью скрыть элемент
            element.Visibility = Visibility.Collapsed;
        }
    }
}