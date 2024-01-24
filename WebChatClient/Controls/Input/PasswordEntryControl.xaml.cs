using System.Diagnostics;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Security;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для TextEntryControl.xaml
    /// </summary>
    public partial class PasswordEntryControl : UserControl
    {
        // Ширина метки элемента управления
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        //Использование свойства зависимостей в качестве резервного хранилища для ширины метки.
        //Это обеспечивает анимацию, стилизацию, привязку и т.д.
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", 
                typeof(GridLength), 
                typeof(PasswordEntryControl), 
                new PropertyMetadata(GridLength.Auto, LabelWidthChangedCallback));
        public PasswordEntryControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вызывается, когда ширина лейбла изменилась.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void LabelWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                // Установите ширину определения столбца на новое значение
                (d as PasswordEntryControl).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }

#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                // Сообщить о потенциальной проблеме
                Debugger.Break();

                (d as PasswordEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }

        /// <summary>
        /// Обновите значение модели представления, указав новый пароль.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Обновить модель представления
            if (DataContext is PasswordEntryVM viewModel)
                viewModel.NewPassword = NewPassword.SecurePassword;
        }

        /// <summary>
        /// Обновите значение модели представления, указав новый пароль.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrrentPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Обновить модель представления
            if (DataContext is PasswordEntryVM viewModel)
                viewModel.CurrentPassword = CurrentPassword.SecurePassword;
        }

        /// <summary>
        /// Обновите значение модели представления, указав новый пароль.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Обновить модель представления
            if (DataContext is PasswordEntryVM viewModel)
                viewModel.ConfirmPassword = ConfirmPassword.SecurePassword;
        }
    }
}
