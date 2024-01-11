using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        // Флаг, указывающий, должна ли эта страница анимироваться при загрузке.
        // Полезно, когда мы перемещаем страницу в другой фрейм.
        public bool ShouldAnimateOut { get; set; }

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
            // Если мы настроены на анимацию при загрузке
            if (ShouldAnimateOut)
                // Анимировать страницу из
                await AnimateOutAsync();
            else
                // Анимировать страницу в
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

            // Старт анимации
            await PageAnimations.SlideAndFadeInFromRightAsync(this, SlideSeconds);
        }

        /// <summary>
        /// Анимирует страницу
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            // Убедитесь, что анимация не закончена
            if (PageUnloadAnimation == PageAnimationStyles.None)
                return;

            // Старт анимации
            await PageAnimations.SlideAndFadeOutToLeftAsync(this, SlideSeconds);
        }
    }
}
