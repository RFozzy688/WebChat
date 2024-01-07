using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Фокусирует (фокус клавиатуры) этот элемент при загрузке
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Если у нас нет control, вернёмся
            if (!(sender is Control control))
                return;

            // Сфокусируйте этот элемент управления после загрузки
            control.Loaded += (s, se) => control.Focus();
        }
    }
}
