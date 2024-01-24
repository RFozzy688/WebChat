using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        ChatPageVM _vm;
        public ChatPage()
        {
            InitializeComponent();

            _vm = new ChatPageVM(this);
            DataContext = _vm;
        }

        /// <summary>
        /// Предварительный просмотр ввода в окне сообщения и ответ по мере необходимости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Получить текстовое поле
            var textbox = sender as TextBox;

            // Проверьте, нажали ли мы Enter
            if (e.Key == Key.Enter)
            {
                // Если у нас нажат Control...
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    // Добавьте новую строку в том месте, где находится курсор
                    var index = textbox.CaretIndex;

                    // Вставьте новую строку
                    textbox.Text = textbox.Text.Insert(index, Environment.NewLine);

                    // Переместить каретку вперед на новую строку
                    textbox.CaretIndex = index + Environment.NewLine.Length;

                    // Отметить этот ключ как обработанный нами
                    e.Handled = true;
                }
                else
                {
                    // Отправить сообщение
                    _vm.Send();
                }

                // Отметить ключ как обработанный
                e.Handled = true;
            }
        }
    }
}
