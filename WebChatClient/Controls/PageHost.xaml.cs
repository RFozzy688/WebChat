using Microsoft.VisualBasic.ApplicationServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        // Текущая страница, которая будет отображаться в PageHost
        public AppPage CurrentPage
        {
            get => (AppPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        /// Регистрирует <see cref="CurrentPage"/> как свойство зависимости.
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), 
                typeof(AppPage), 
                typeof(PageHost), 
                new UIPropertyMetadata(default(AppPage), null, CurrentPagePropertyChanged));

        // Текущая страница, отображаемая на хосте страниц
        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        /// <summary>
        /// Регистрирует <see cref="Текущая модель просмотра страницы"/> как свойство зависимости
        /// </summary>
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel),
                typeof(BaseViewModel), typeof(PageHost),
                new UIPropertyMetadata());

        public PageHost()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Вызывается, когда значение <see cref="CurrentPage"/> изменилось.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            // Get current values
            var currentPage = (AppPage)d.GetValue(CurrentPageProperty);
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            // Получите кадры
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            // Если текущая страница не изменилась, просто обновите модель представления
            //if (newPageFrame.Content is BasePage page && page.ToApplicationPage() == currentPage)
            //{
            //    // Just update the view model
            //    page.ViewModelObject = currentPageViewModel;

            //    return value;
            //}

            // Сохраните содержимое текущей страницы как старую страницу.
            var oldPageContent = newPageFrame.Content;

            // Удалить текущую страницу из нового фрейма страницы
            newPageFrame.Content = null;

            // Переместить предыдущую страницу в рамку старой страницы
            oldPageFrame.Content = oldPageContent;

            // Анимировать предыдущую страницу, событие Loaded срабатывает
            // сразу после этого вызова из-за перемещения кадров
            if (oldPageContent is BasePage oldPage)
            {
                // анимировать старую страницу 
                oldPage.ShouldAnimateOut = true;

                // Как только это будет сделано, удалите его
                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((t) =>
                {
                    // Удалить старую страницу
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            // Установите новое содержимое страницы
            newPageFrame.Content = new AppPageValueConverter().Convert(currentPage, null, currentPageViewModel, null);

            return value;
        }
    }
}
