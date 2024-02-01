using System.Text.Json;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для экрана входа в систему
    /// </summary>
    public class RegisterPageVM : BaseViewModel
    {
        // email пользователя
        public string Email { get; set; } = string.Empty;

        // Имя пользователя
        public string Username { get; set; } = string.Empty;

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
            // от повторного нажатия кнопки
            if (RegisterIsRunning)
                return;

            RegisterIsRunning = true;

            // проверка на заполнение TextBox-ов
            if (!IsValidTextBoxes(parameter))
            {
                RegisterIsRunning = false;
                return;
            }

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
            }
            else
            {
                MessageBoxModel.Title = "Ошибка регистрации";
                MessageBoxModel.Message = str;

                DialogMessageBox dialog = new DialogMessageBox();
                dialog.ShowDialog();
            }

            RegisterIsRunning = false;

            // отписаться
            WorkWithServer.ResponceEvent -= UserRegistration;
        }

        // валидность почты
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z]+\.[a-zA-Z]";
            Match match = Regex.Match(email, pattern);

            return match.Success;
        }

        // Правильно ли заполнены TextBox-сы. Пока все поля не будут заполнены правильно
        // будет выводится сообщение
        private bool IsValidTextBoxes(object parameter)
        {
            string errorMessage;

            // никнейм
            if (Username.Length <= 3)
            {
                errorMessage = "Имя должно быть не меньше 4-х символов";
            }
            // почта
            else if (!IsValidEmail(Email))
            {
                errorMessage = "Неверный формат почты";
            }
            // пароль
            else if ((parameter as IHavePassword).SecurePassword.Unsecure().Length <= 5)
            {
                errorMessage = "Пароль должен быть не меньше 6-и символов";
            }
            else
            {
                return true;
            }

            MessageBoxModel.Title = "Ошибка регистрации";
            MessageBoxModel.Message = errorMessage;

            DialogMessageBox dialog = new DialogMessageBox();
            dialog.ShowDialog();

            return false;
        }
    }
}
