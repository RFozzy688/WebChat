using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для экрана входа в систему
    /// </summary>
    public class RegisterPageVM : BaseViewModel
    {
        RegisterPage _view;
        // email пользователя
        public string Email { get; set; }

        // Имя пользователя
        public string Username { get; set; }

        // Телефон пользователя
        public string Phone { get; set; }

        /// Флаг, указывающий, выполняется ли команда входа в систему.
        public bool RegisterIsRunning { get; set; } = false;

        // команда регистрации
        public ICommand RegisterCommand { get; set; }
        // команда входа
        public ICommand LoginCommand { get; set; }

        public RegisterPageVM(/*RegisterPage view*/)
        {
            //_view = view;
            // Создать команду
            RegisterCommand = new Command(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new Command(async (parameter) => await LoginAsync(parameter));
        }

        private async Task LoginAsync(object parameter)
        {
            //((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = AppPage.Login;
            IoC.Get<AppVM>().GoToPage(AppPage.Login);

            await Task.Delay(1);
        }

        /// <summary>
        /// Попытки войти в систему пользователя
        /// </summary>
        /// <param name="parameter"><see cref="SecureString"/>, переданный из представления пароля 
        /// пользователя.</param>
        /// <returns></returns>
        public async Task RegisterAsync(object parameter)
        {
            if (RegisterIsRunning)
                return;

            RegisterIsRunning = true;

            await Task.Delay(5000);
            var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            var name = Username;
            var phone = Phone;
            var email = Email;

            RegisterIsRunning = false;
        }
    }
}
