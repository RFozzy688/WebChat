using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Прикрепленное свойство MonitorPassword для <смотри cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Получить пароль
            var passwordBox = sender as PasswordBox;

            // Проверить валидность приведения
            if (passwordBox == null)
                return;

            // Удалить все предыдущие события
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // Если PasswordBox установил для MonitorPassword значение true...
            if ((bool)e.NewValue)
            {
                // Установить значение по умолчанию
                HasTextProperty.SetValue(passwordBox);

                // Начинаем прослушивать изменения в PasswordBox
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Вызывается при изменении значения пароля в PasswordBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Установить прикрепленное значение HasText
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// Прикрепленное свойство HasText для <смотри cref="PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// Устанавливает свойство HasText в зависимости от того, есть ли у вызывающего объекта 
        /// <смотри cref="PasswordBox"/> какой-либо текст.
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
}
