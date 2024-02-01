using System;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для экрана входа в систему
    /// </summary>
    public class LoginPageVM : BaseViewModel
    {
        // email пользователя
        public string Email { get; set; }

        // Флаг, указывающий, выполняется ли команда входа в систему.
        public bool LoginIsRunning { get; set; } = false;

        // команда входа
        public ICommand LoginCommand { get; set; }
        // команда регистрации
        public ICommand RegisterCommand { get; set; }

        public LoginPageVM()
        {
            // Создать команду
            LoginCommand = new Command(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new Command(async (parameter) => await RegisterAsync(parameter));
        }

        // переход на страницу регистрации
        private async Task RegisterAsync(object parameter)
        {
            // открыть страницу регистрации
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new RegisterPage();

            await Task.Delay(1);
        }

        /// <summary>
        /// Попытки войти в систему пользователя
        /// </summary>
        /// <param name="parameter"><see cref="SecureString"/>, переданный из представления пароля 
        /// пользователя.</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            if (LoginIsRunning)
                return;

            // Флаг, указывающий, выполняется ли команда входа в систему
            LoginIsRunning = true;

            // получить данные с TextBox-в
            UserAuthorization user = new UserAuthorization();
            user.Password = (parameter as IHavePassword).SecurePassword.Unsecure();
            user.Email = Email;

            // сформировать данные для отправки на сервер
            DataPackage package = new DataPackage();
            // тип пакета
            package.Package = TypeData.Authorization;
            // основные данные пакета
            package.StringSerialize = JsonSerializer.Serialize(user);

            // подписаться на событие о приходе ответа с сервера
            WorkWithServer.ResponceEvent += UserAuthorization;
            // отправить данные на сервер
            await WorkWithServer.SendMessageAsync(JsonSerializer.Serialize(package));
        }

        // метод вызывается по событию от сервера
        private void UserAuthorization(string str)
        {
            if (str.CompareTo("true") == 0)
            {
                // если истина, то входим в чат
                ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new ChatPage();
            }
            else
            {
                MessageBoxModel.Title = "Ошибка авторизации";
                MessageBoxModel.Message = "Неверный логин или пароль";

                DialogMessageBox dialog = new DialogMessageBox();
                dialog.ShowDialog();
            }

            LoginIsRunning = false;

            // отписаться
            WorkWithServer.ResponceEvent -= UserAuthorization;
        }
    }
}
