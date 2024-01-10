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
        // Команда открытия меню настроек
        public ICommand OpenCommand { get; set; }

        // Команда закрытия меню настроек
        public ICommand CloseCommand { get; set; }

        public SettingsVM()
        {
            // Создание команд
            OpenCommand = new Command((o) => Open());
            CloseCommand = new Command((o) => Close());
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
