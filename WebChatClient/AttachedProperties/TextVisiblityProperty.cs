using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WebChatClient
{
    /// <summary>
    /// Создает область отсечения из родительского элемента <see cref="Border"/> <see cref="CornerRadius"/>
    /// </summary>
    public class TextVisiblityProperty : BaseAttachedProperty<TextVisiblityProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // скрываем/показываем TextBox
            if (sender is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    textBox.Visibility = Visibility.Visible;
                    // сфокусировать на TextBox
                    textBox.Focus();
                    // выделить контент в TextBox
                    textBox.SelectAll();
                }
            }

            // скрываем/показываем TextBlock
            if (sender is TextBlock textBlock)
            {
                if ((bool)e.NewValue)
                {
                    textBlock.Visibility = Visibility.Hidden;
                }
                else
                {
                    textBlock.Visibility = Visibility.Visible;
                }
            }

            // скрываем/показываем StackPanel
            if (sender is StackPanel stackPanel)
            {
                if ((bool)e.NewValue)
                {
                    stackPanel.Visibility = Visibility.Hidden;
                }
                else
                {
                    stackPanel.Visibility = Visibility.Visible;
                }
            }

            // скрываем/показываем Button
            if (sender is Button button)
            {
                if ((bool)e.NewValue)
                {
                    button.Visibility = Visibility.Hidden;
                }
                else
                {
                    button.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
