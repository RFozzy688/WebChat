﻿using System.Security;
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

        /// Флаг, указывающий, выполняется ли команда входа в систему.
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

        private async Task RegisterAsync(object parameter)
        {
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

            LoginIsRunning = true;

            await Task.Delay(1);
            var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            var email = Email;

            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new ChatPage();

            LoginIsRunning = false;
        }
    }
}