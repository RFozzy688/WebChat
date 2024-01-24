using System.Security;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IHavePassword
    {
        public RegisterPage()
        {
            InitializeComponent();

            DataContext = new RegisterPageVM();
        }

        // Надежный пароль для этой страницы входа.
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
