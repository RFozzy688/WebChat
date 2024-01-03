using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WebChatClient
{
    /// <summary>
    /// Помощники для анимации страниц определенными способами
    /// </summary>
    public static class PageAnimations
    {
        /// <summary>
        /// Сдвигает страницу справа
        /// </summary>
        /// <param name="page">Страница для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(Page page, float seconds)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из правой анимации
            sb.AddSlideFromRight(seconds, page.WindowWidth);

            // Добавить затухание анимации
            sb.AddFadeIn(seconds);

            // Начать анимацию
            sb.Begin(page);

            // Сделать страницу видимой
            page.Visibility = Visibility.Visible;

            // Ждать, пока закончится анимация
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Сдвигает страницу влево
        /// </summary>
        /// <param name="page">Страница для анимации</param>
        /// <param name="seconds">Время, которое займет анимация</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeftAsync(Page page, float seconds)
        {
            // Создайте раскадровку
            var sb = new Storyboard();

            // Добавить слайд из левой анимации
            sb.AddSlideToLeft(seconds, page.WindowWidth);

            // Добавить затухание анимации
            sb.AddFadeOut(seconds);

            // Начать анимацию
            sb.Begin(page);

            // Сделать страницу видимой
            page.Visibility = Visibility.Visible;

            // Ждать, пока закончится анимация
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
