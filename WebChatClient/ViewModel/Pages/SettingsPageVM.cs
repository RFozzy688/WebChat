using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    public class SettingsPageVM
    {
        // представление чата
        ChatPage _chatPage;

        // пердставление страницы настроек
        SettingsPage _settingsPage;

        // модель данных пользователя
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
            LogoutCommand = new Command((o) => Logout());
            ClearUserDataCommand = new Command((o) => ClearUserData());

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

            // ник в сети
            Nickname = new TextEntryVM();
            Nickname.Label = "Nickname";
            Nickname.OriginalText = _detailsProfile.Nickname;

            // пароль
            Password = new PasswordEntryVM();
            Password.Label = "Password";
            Password.FakePassword = _detailsProfile.Password;

            // ник в сети
            Email = new TextEntryVM();
            Email.Label = "Email";
            Email.OriginalText = _detailsProfile.Email;
        }

        private void Close()
        {
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = _chatPage;
        }

        // Выводит пользователя из системы
        private void Logout()
        {
            // Очистите все модели представления уровня приложения, содержащие
            // любую информацию о текущем пользователе
            ClearUserData();

            // Go to login page
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new LoginPage();
        }

        // Очищает любые данные, относящиеся к текущему пользователю
        public void ClearUserData()
        {
            // Очистить все модели представления, содержащие информацию о пользователях
            Name = null;
            Nickname = null;
            Password = null;
            Email = null;
        }
    }
}
