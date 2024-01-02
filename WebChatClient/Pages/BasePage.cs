using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WebChatClient
{
    /// <summary>
    /// Базовая страница для получения базовой функциональности.
    /// </summary>
    public class BasePage : Page
    {
        // Анимация, которая воспроизводится при первой загрузке страницы.
        public PageAnimationStyles PageLoadAnimation { get; set; } 
            = PageAnimationStyles.MovesFromRightToCenter;

        // Анимация, воспроизводимая при выгрузке страницы
        public PageAnimationStyles PageUnloadAnimation { get; set; } 
            = PageAnimationStyles.MovesFromCenterToLeft;

        // Время, анимации слайда
        public float SlideSeconds { get; set; } = 0.4f;

        // Конструктор по умолчанию
        public BasePage()
        {
            // Если мы анимируем, то в начале она скрыта
            if (PageLoadAnimation != PageAnimationStyles.None)
                Visibility = Visibility.Collapsed;

            // Обработчик события загрузки страницы
            Loaded += BasePage_LoadedAsync;
        }

        /// <summary>
        /// После загрузки страницы выполните любую необходимую анимацию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // Анимировать справа к центру
            await AnimateInAsync();
        }

        /// <summary>
        /// Анимировать справа к центру
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Анимация закончена
            if (PageLoadAnimation == PageAnimationStyles.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimationStyles.MovesFromRightToCenter:
                    var sb = new Storyboard();
                    var slideAnimation = new ThicknessAnimation
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(SlideSeconds)),
                        From = new Thickness(WindowWidth, 0, -WindowWidth, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0.9f
                    };

                    Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
                    sb.Children.Add(slideAnimation);

                    sb.Begin(this);
                    Visibility = Visibility.Visible;
                    await Task.Delay((int)SlideSeconds * 1000);
                    break;
            }
        }
    }
}
