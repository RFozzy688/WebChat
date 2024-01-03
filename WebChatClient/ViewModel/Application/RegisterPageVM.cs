using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        // команда входа
        public ICommand RegisterCommand { get; set; }

        public RegisterPageVM(RegisterPage view)
        {
            _view = view;
            // Создать команду
            RegisterCommand = new Command(async (parameter) => await RegisterAsync(parameter));
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
