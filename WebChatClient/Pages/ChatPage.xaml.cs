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
        }
    }
}
