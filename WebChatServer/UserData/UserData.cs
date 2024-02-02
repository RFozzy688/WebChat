using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatServer
{
    // данные пользователя для регистрации на сервере
    public class UserRegistration
    {
        // ник пользователя
        public string Nickname { get; set; }

        // почта пользователя
        public string Email { get; set; }

        // пароль пользователя
        public string Password { get; set; }
    }

    // данные пользователя для авторизации на сервере
    public class UserAuthorization
    {
        // почта пользователя
        public string Email { get; set; }

        // пароль пользователя
        public string Password { get; set; }
    }

    // данные для верификации почты на сервере
    public class UserVerificationEmail
    {
        // почта пользователя
        public string Email { get; set; }

        // код верификации
        public string Code { get; set; }
    }

    // данные для поиска пользователя в бд
    public class FindUser
    {
        // email user
        public string Email { get; set; }

        // уникальный идентификатор пользователя в этом приложении
        public string UserID { get; set; }

        // Отображаемое имя в списке чатов
        public string Name { get; set; }
    }
}
