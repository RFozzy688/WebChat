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
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние вправо, начиная с</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            //  Создайте анимацию поля справа
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Установите имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет слайд из левой анимации в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <param name="offset">Расстояние с лева, начиная с</param>
        /// <param name="decelerationRatio">Скорость замедления</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            //  Создайте анимацию поля с лева
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelerationRatio
            };

            // Установите имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Добавьте это в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет плавную анимацию в раскадровку.
        /// </summary>
        /// <param name="storyboard">Раскадровка, к которой нужно добавить анимацию.</param>
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

            // Установите имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Добавить в раскадровку
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Добавляет анимацию затухания в раскадровку.
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            //  Создайте анимацию поля справа
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                To = 0,
            };

            // Установите имя целевого свойства
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Добавить в раскадровку
            storyboard.Children.Add(animation);
        }
    }
}
