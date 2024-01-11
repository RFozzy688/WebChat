using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq.Expressions;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
using Ninject.Planning;
using System.ComponentModel;
using WebChatCore;
using System.Windows.Markup;

namespace WebChatClient
{
    /// <summary>
    /// Состояние настроек как модель представления
    /// </summary>
    public class SettingsVM : BaseViewModel
    {
        // Имя текущего пользователя
        public TextEntryVM Name { get; set; }

        // Имя пользователя текущего пользователя
        public TextEntryVM Username { get; set; }

        // Текущий пароль пользователя
        public PasswordEntryVM Password { get; set; }

        // Электронная почта текущего пользователя
        public TextEntryVM Email { get; set; }

        // Текст для кнопки выхода из системы
        public string LogoutButtonText { get; set; }

        // Команда открытия меню настроек
        public ICommand OpenCommand { get; set; }

        // Команда закрытия меню настроек
        public ICommand CloseCommand { get; set; }

        // Команда выхода из приложения
        public ICommand LogoutCommand { get; set; }

        // Команда для очистки данных пользователей из модели представления
        public ICommand ClearUserDataCommand { get; set; }

        public SettingsVM()
        {
            // Создание команд
            OpenCommand = new Command((o) => Open());
            CloseCommand = new Command((o) => Close());
            LogoutCommand = new Command((o) => Logout());
            ClearUserDataCommand = new Command((o) => ClearUserData());

            LogoutButtonText = "Выход из системы";

            // TODO: Удалите это
            Name = new TextEntryVM { Label = "Name", OriginalText = "Luke Malpass" };
            Username = new TextEntryVM { Label = "Username", OriginalText = "luke" };
            Password = new PasswordEntryVM { Label = "Password", FakePassword = "********" };
            Email = new TextEntryVM { Label = "Email", OriginalText = "contact@angelsix.com" };
        }

        // Выводит пользователя из системы
        private void Logout()
        {
            // Очистите все модели представления уровня приложения, содержащие
            // любую информацию о текущем пользователе
            ClearUserData();

            // Go to login page
            IoC.Application.GoToPage(AppPage.Login);
        }

        // Откройте меню настроек
        public void Open()
        {
            // Откройть меню настроек
            IoC.Application.SettingsMenuVisible = true;
        }

        // Закрывает меню настроек
        public void Close()
        {
            // Закрыть меню настроек
            IoC.Application.SettingsMenuVisible = false;
        }

        // Очищает любые данные, относящиеся к текущему пользователю
        public void ClearUserData()
        {
            // Очистить все модели представления, содержащие информацию о пользователях
            Name = null;
            Username = null;
            Password = null;
            Email = null;
        }
    }
}
