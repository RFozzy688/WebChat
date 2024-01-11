using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Прикрепленное свойство NoFrameHistory для создания <see cref="Frame"/>, который никогда 
    /// не отображает навигацию и сохраняет историю навигации пустой.
    /// </summary>
    public class PanelChildMarginProperty : BaseAttachedProperty<PanelChildMarginProperty, string>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Получите панель (обычно сетку)
            var panel = (sender as Panel);

            //Подождите, пока загрузится панель
            panel.Loaded += (s, ee) =>
            {
                // Зациклить каждого ребенка
                foreach (var child in panel.Children)
                {
                    // Установите для поля заданное значение
                    (child as FrameworkElement).Margin = (Thickness)(new ThicknessConverter().ConvertFromString(e.NewValue as string));
                }
            };
        }
    }
}
