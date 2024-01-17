using System.Windows.Controls;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Прикрепленное свойство NoFrameHistory для создания <see cref="Frame"/>, который никогда 
    /// не отображает навигацию и сохраняет историю навигации пустой.
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Получить кадр
            var frame = (sender as Frame);

            // Скрыть панель навигации
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            // Очистить историю при навигации
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }
}
