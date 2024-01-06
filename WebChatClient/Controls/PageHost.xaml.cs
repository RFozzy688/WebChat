using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        /// <summary>
        /// Текущая страница, которая будет отображаться в PageHost
        /// </summary>
        public BasePage CurrentPage
        {
            get => (BasePage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        /// Регистрирует <see cref="CurrentPage"/> как свойство зависимости.
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(BasePage), typeof(PageHost), new UIPropertyMetadata(CurrentPagePropertyChanged));

        public PageHost()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Вызывается, когда значение <see cref="CurrentPage"/> изменилось.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void CurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Получите кадры
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

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
                oldPage.ShouldAnimateOut = true;
            }

            // Установите новое содержимое страницы
            newPageFrame.Content = e.NewValue;
        }
    }
}
