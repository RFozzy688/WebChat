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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
