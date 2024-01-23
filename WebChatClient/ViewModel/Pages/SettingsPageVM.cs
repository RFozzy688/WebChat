using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    public class SettingsPageVM
    {
        ChatPage _chatPage;
        SettingsPage _settingsPage;
        DetailsProfileModel _detailsProfile;

        // Имя текущего пользователя
        public TextEntryVM Name { get; set; }

        // Имя пользователя текущего пользователя
        public TextEntryVM Nickname { get; set; }

        // Текущий пароль пользователя
        public PasswordEntryVM Password { get; set; }

        // Электронная почта текущего пользователя
        public TextEntryVM Email { get; set; }

        // Текст для кнопки выхода из системы
        public string LogoutButtonText { get; set; } = "Выход из системы";

        // Команда открытия меню настроек
        public ICommand OpenCommand { get; set; }

        // Команда закрытия меню настроек
        public ICommand CloseCommand { get; set; }

        // Команда выхода из приложения
        public ICommand LogoutCommand { get; set; }

        // Команда для очистки данных пользователей из модели представления
        public ICommand ClearUserDataCommand { get; set; }

        public SettingsPageVM(ChatPage chatPage, SettingsPage settingsPage)
        {
            // Создание команд
            CloseCommand = new Command((o) => Close());

            _chatPage = chatPage;
            _settingsPage = settingsPage;

            // загрузка профиля пользователя для редактирования
            LoadingUserProfile();
        }

        private void LoadingUserProfile()
        {
            _detailsProfile = new DetailsProfileModel();

            // полное имя
            Name = new TextEntryVM();
            Name.Label = "Name";
            Name.OriginalText = _detailsProfile.Name;
            _settingsPage.TextEntryName.DataContext = Name;

            // ник в сети
            Nickname = new TextEntryVM();
            Nickname.Label = "Nickname";
            Nickname.OriginalText = _detailsProfile.Nickname;
            _settingsPage.TextEntryNickname.DataContext = Nickname;

            // пароль
            Password = new PasswordEntryVM();
            Password.Label = "Password";
            Password.FakePassword = _detailsProfile.Password;
            _settingsPage.PasswordEntry.DataContext = Password;

            // ник в сети
            Email = new TextEntryVM();
            Email.Label = "Email";
            Email.OriginalText = _detailsProfile.Email;
            _settingsPage.TextEntryEmail.DataContext = Email;
        }

        private void Close()
        {
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = _chatPage;
        }
    }
}
