using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatClient
{
    // тип пакета данных, говорит серверу как десериализовывать строку с данными
    public enum TypeData
    {
        Message,
        Registration,
        Authorization
    }

    // пакет данных который непосредственно отправляется на сервер
    public class DataPackage
    {
        // тип данных для выбора типа десериализации строки с данными
        public TypeData Package { get; set; }

        // строка с сериализованными данными
        public string StringSerialize { get; set; }
    }

    // сообщение отправляемое своему контакту
    public class UserMessage
    {
        // от кого
        public string UserId { get; set; }

        // кому
        public string RecipientId { get; set; }

        // сообщение
        public string Message { get; set; }
    }

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
}
