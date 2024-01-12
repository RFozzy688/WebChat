using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : BasePage<ChatMessageListVM>
    {
        public ChatPage() : base()
        {
            InitializeComponent();

            //DataContext = new ChatMessageListVM();
        }

        /// <summary>
        /// Конструктор с конкретной моделью представления
        /// </summary>
        /// <param name="specificViewModel">Конкретная модель представления, используемая для этой страницы.</param>
        public ChatPage(ChatMessageListVM specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Запускается при изменении модели представления
        /// </summary>
        protected override void OnViewModelChanged()
        {
            // Сначала убедитесь, что пользовательский интерфейс существует
            if (ChatMessageList == null)
                return;

            // Затухание списка сообщений чата
            var storyboard = new Storyboard();
            storyboard.AddFadeIn(0.5f);
            storyboard.Begin(ChatMessageList);

            // Сделайте окно сообщения сфокусированным
            MessageText.Focus();
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
                    // Send the message
                    ViewModel.Send();
                }

                // Отметить ключ как обработанный
                e.Handled = true;
            }
        }
    }
}
