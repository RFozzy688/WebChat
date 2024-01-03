﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public LoginPageVM()
        {
            // Создать команду
            LoginCommand = new Command(async (parameter) => await LoginAsync(parameter));
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

            await Task.Delay(5000);
            var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            var email = Email;

            LoginIsRunning = false;
        }
    }
}
