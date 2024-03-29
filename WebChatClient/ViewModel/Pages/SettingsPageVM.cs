﻿using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    public class SettingsPageVM
    {
        // представление чата
        ChatPage _chatPage;

        // пердставление страницы настроек
        //SettingsPage _settingsPage;

        // модель данных пользователя
        //DetailsProfileModel _detailsProfile;

        // Имя текущего пользователя
        public TextEntryVM Name { get; set; }

        // Имя пользователя текущего пользователя
        public TextEntryVM Nickname { get; set; }

        // Текущий пароль пользователя
        public PasswordEntryVM Password { get; set; }

        // Электронная почта текущего пользователя
        public EmailEntryVM Email { get; set; }

        // Текст для кнопки выхода из системы
        public string LogoutButtonText { get; set; } = "Выход из системы";

        // Команда закрытия меню настроек
        public ICommand CloseCommand { get; set; }

        // Команда выхода из приложения
        public ICommand LogoutCommand { get; set; }

        // Команда для очистки данных пользователей из модели представления
        public ICommand ClearUserDataCommand { get; set; }

        public SettingsPageVM(ChatPage chatPage/*, SettingsPage settingsPage*/)
        {
            // Создание команд
            CloseCommand = new Command((o) => Close());
            LogoutCommand = new Command((o) => Logout());
            ClearUserDataCommand = new Command((o) => ClearUserData());

            _chatPage = chatPage;
            //_settingsPage = settingsPage;

            // загрузка профиля пользователя для редактирования
            LoadingUserProfile();
        }

        // загрузка профиля пользователя для редактирования
        private void LoadingUserProfile()
        {
            // полное имя
            Name = new TextEntryVM();
            Name.Label = "Name";
            Name.OriginalText = DetailsProfileModel.Name;

            // ник в сети
            Nickname = new TextEntryVM();
            Nickname.Label = "Nickname";
            Nickname.OriginalText = DetailsProfileModel.Nickname;

            // пароль
            Password = new PasswordEntryVM();
            Password.Label = "Password";
            Password.FakePassword = DetailsProfileModel.Password;

            // email
            Email = new EmailEntryVM();
            Email.Label = "Email";
            Email.Email = DetailsProfileModel.Email;
        }

        // закрыть страницу настроек
        private void Close()
        {
            ChatPage chatPage;

            if (_chatPage != null)
            {
                // при выходе из страницы настроек возвращаемся на старую страницу чата
                 chatPage = _chatPage;
            }
            else
            {
                // если верификация почты прошла успешно создаем новую страницу чата
                chatPage = new ChatPage();
            }
             // отобразить страницу чата
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = chatPage;
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
