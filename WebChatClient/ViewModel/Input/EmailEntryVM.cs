using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления текстовой записи для редактирования строкового значения
    /// <summary>
    public class EmailEntryVM : BaseViewModel
    {
        // Метка, определяющая, для чего предназначено это значение
        public string Label { get; set; }

        // Текущая почта пользователя
        public string Email { get; set; }

        // Код верификации
        public string VerifyingCode { get; set; }

        // Указывает, находится ли текущий текст в режиме верификации
        public bool Editing { get; set; }

        // Открывает TextBox для ввода кода верификации
        public ICommand OpenVerificationCommand { get; set; }

        // Выход из режима верификации
        public ICommand CancelCommand { get; set; }

        // Отправляет код верификации на сервер
        public ICommand SendCodeCommand { get; set; }

        public EmailEntryVM()
        {
            // Создание команд
            OpenVerificationCommand = new Command((o) => OpenVerification());
            CancelCommand = new Command((o) => Cancel());
            SendCodeCommand = new Command(async (o) => await SendCodeAsync());
        }

        // Переводит элемент управления в режим верификации
        public void OpenVerification()
        {
            // Перейти в режим верификации
            Editing = true;
        }

        // Выход из режима верификации
        public void Cancel()
        {
            Editing = false;
        }

        // Отправляет код и выходит из режима редактирования
        public async Task SendCodeAsync()
        {
            Editing = false;

            // получить данные для верификации почты
            VerificationEmail verification = new VerificationEmail();
            verification.Email = Email;
            verification.Code = VerifyingCode;

            // сформировать данные для отправки на сервер
            DataPackage package = new DataPackage();
            // тип пакета
            package.Package = TypeData.Verification;
            // основные данные пакета
            package.StringSerialize = JsonSerializer.Serialize(verification);

            // подписаться на событие о приходе ответа с сервера
            WorkWithServer.ResponceEvent += UserVerificationEmail;
            // отправить данные на сервер
            await WorkWithServer.SendMessageAsync(JsonSerializer.Serialize(package));
        }

        // метод вызывается по событию от сервера
        private void UserVerificationEmail(string str)
        {
            if (str.CompareTo("true") == 0)
            {
                // если истина, то входим на страницу настроек для верификации почты
                str = "Почта верифицированна!!!";

                // отписаться
                WorkWithServer.ResponceEvent -= UserVerificationEmail;
            }

            MessageBoxModel.Title = "Верификация почты";
            MessageBoxModel.Message = str;

            DialogMessageBox dialog = new DialogMessageBox();
            dialog.ShowDialog();
        }
    }
}
