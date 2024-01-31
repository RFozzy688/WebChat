using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        ChatPage _chatPage;
        public SettingsPage(ChatPage chatPage)
        {
            InitializeComponent();

            _chatPage = chatPage;

            DataContext = new SettingsPageVM(chatPage/*, this*/);
        }
    }
}
