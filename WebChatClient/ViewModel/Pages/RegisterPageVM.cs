using System.Text.Json;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для экрана входа в систему
    /// </summary>
    public class RegisterPageVM : BaseViewModel
    {
        // email пользователя
        public string Email { get; set; }

        // Имя пользователя
        public string Username { get; set; }

        // Флаг, указывающий, выполняется ли команда входа в систему.
        public bool RegisterIsRunning { get; set; } = false;

        // команда регистрации
        public ICommand RegisterCommand { get; set; }
        // команда входа
        public ICommand LoginCommand { get; set; }

        public RegisterPageVM()
        {
            // Создать команду
            RegisterCommand = new Command(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new Command(async (parameter) => await LoginAsync(parameter));
        }

        // перейти на страницу входа
        private async Task LoginAsync(object parameter)
        {
            // отобразить страницу входа
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new LoginPage();

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

            UserRegistration user = new UserRegistration();
            user.Password = (parameter as IHavePassword).SecurePassword.Unsecure();
            user.Nickname = Username;
            user.Email = Email;

            // сформировать данные для отправки на сервер
            DataPackage package = new DataPackage();
            // тип пакета
            package.Package = TypeData.Registration;
            // основные данные пакета
            package.StringSerialize = JsonSerializer.Serialize(user);

            // подписаться на событие о приходе ответа с сервера
            WorkWithServer.ResponceEvent += UserRegistration;
            // отправить данные на сервер
            await WorkWithServer.SendMessageAsync(JsonSerializer.Serialize(package));
        }

        // метод вызывается по событию от сервера
        private void UserRegistration(string str)
        {
            if (str.CompareTo("true") == 0)
            {
                // если истина, то входим на страницу настроек для верификации почты
                ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new SettingsPage(null);

                // отписаться
                WorkWithServer.ResponceEvent -= UserRegistration;
            }
            else
            {
                MessageBoxModel.Title = "Ошибка регистрации";
                MessageBoxModel.Message = str;

                DialogMessageBox dialog = new DialogMessageBox();
                dialog.ShowDialog();
            }

            RegisterIsRunning = false;
        }
    }
}
