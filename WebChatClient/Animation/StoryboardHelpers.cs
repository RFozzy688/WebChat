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
    /// Помощники по анимации для <see cref="StoryBoard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Добавляет слайд из правой анимации в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние вправо, начиная с</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет анимацию слайда вправо в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние вправо до конца</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        public static void AddSlideToRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Добавляет слайд из левой анимации в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние слева от начала</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        public static void AddSlideFromLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(-offset, 0,  keepMargin ? offset : 0, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет анимацию слайда слева в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние слева до конца</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент одинаковой ширины во время анимации</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0,  keepMargin ? offset : 0, 0),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Добавляет слайд из нижней анимации в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние до низа, начиная с которого</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент на той же высоте во время анимации</param>
        public static void AddSlideFromBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет анимацию слайда вниз в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние до низа, где заканчивается</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        /// <param name="keepMargin">Сохранять ли элемент на той же высоте во время анимации</param>
        public static void AddSlideToBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Создайте анимацию поля справа
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                DecelerationRatio = decelerationRatio
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Добавляет плавную анимацию в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // Создайте анимацию поля справа 
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет анимацию затухания в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            // Создайте анимацию поля справа
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };

            // Задайте имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }
    }
}
