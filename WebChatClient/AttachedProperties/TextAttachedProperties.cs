using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            {
                return;
            }

            // Сфокусируйте этот элемент управления после загрузки
            control.Loaded += (s, se) => control.Focus();
        }
    }

    /// <summary>
    /// Фокусирует (фокус клавиатуры) этот элемент, если true
    /// </summary>
    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Если у нас нет контроля, вернёмся
            if (!(sender is Control control))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                // Фокусирует этот элемент управления
                control.Focus();
            }
        }
    }

    /// <summary>
    /// Фокусируется (фокус клавиатуры) и выделяет весь текст в этом элементе, если это правда
    /// </summary>
    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If we don't have a control, return
            if (sender is TextBoxBase control)
            {
                if ((bool)e.NewValue)
                {
                    // Фокусирует этот элемент управления
                    control.Focus();

                    // Выделить весь текст
                    control.SelectAll();
                }
            }
            if (sender is PasswordBox password)
            {
                if ((bool)e.NewValue)
                {
                    // Фокусирует этот элемент управления
                    password.Focus();

                    // Выбрать весь текст
                    password.SelectAll();
                }
            }
        }
    }
}
