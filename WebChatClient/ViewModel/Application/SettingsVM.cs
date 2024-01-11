using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq.Expressions;
using System.Diagnostics;

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
        public TextEntryVM Password { get; set; }

        // Электронная почта текущего пользователя
        public TextEntryVM Email { get; set; }

        // Команда открытия меню настроек
        public ICommand OpenCommand { get; set; }

        // Команда закрытия меню настроек
        public ICommand CloseCommand { get; set; }

        public SettingsVM()
        {
            // Создание команд
            OpenCommand = new Command((o) => Open());
            CloseCommand = new Command((o) => Close());

            Name = new TextEntryVM { Label = "Name", OriginalText = "Luke Malpass" };
            Username = new TextEntryVM { Label = "Username", OriginalText = "luke" };
            Password = new TextEntryVM { Label = "Password", OriginalText = "********" };
            Email = new TextEntryVM { Label = "Email", OriginalText = "contact@angelsix.com" };
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
    }
}
