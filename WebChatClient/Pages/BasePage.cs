using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Базовая страница для получения базовой функциональности.
    /// </summary>
    public class BasePage : UserControl
    {
        // Модель представления, связанная с этой страницей
        private object _viewModel;

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

        // Модель представления, связанная с этой страницей
        public object ViewModelObject
        {
            get => _viewModel;
            set
            {
                // Если ничего не изменилось, вернитесь
                if (_viewModel == value)
                    return;

                // Обновить значение
                _viewModel = value;

                // Запустите измененный метод модели представления
                OnViewModelChanged();

                // Установите контекст данных для этой страницы
                DataContext = _viewModel;
            }
        }

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

            switch (PageLoadAnimation)
            {
                case PageAnimationStyles.MovesFromRightToCenter:
                    // Старт анимации
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, 
                        false, 
                        SlideSeconds, 
                        size: (int)Application.Current.MainWindow.Width);
                    break;
            }
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

            switch (PageUnloadAnimation)
            {
                case PageAnimationStyles.MovesFromCenterToLeft:
                    // Старт анимации
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);
                    break;
            }
        }

        // Запускается при изменении модели представления
        protected virtual void OnViewModelChanged()
        {

        }
    }

    /// <summary>
    /// Базовая страница с добавленной поддержкой модели представления
    /// </summary>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
 
        // Модель представления, связанная с этой страницей
        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        public BasePage() : base()
        {
            // Создайте модель представления по умолчанию
            ViewModel = IoC.Get<VM>();
        }

        /// <summary>
        /// Конструктор с конкретной моделью представления
        /// </summary>
        /// <param name="specificViewModel">Конкретная модель представления, которую следует использовать, если таковая имеется.</param>
        public BasePage(VM specificViewModel = null) : base()
        {
            // Установить конкретную модель представления
            if (specificViewModel != null)
            {
                ViewModel = specificViewModel;
            }
            else
            {
                // Создайте модель представления по умолчанию
                ViewModel = IoC.Get<VM>();
            }
        }
    }
}
