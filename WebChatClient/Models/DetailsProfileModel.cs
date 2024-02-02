
using System.Security;

namespace WebChatClient
{
    public static class DetailsProfileModel
    {
        // уникальный идентификатор
        public static string UserID { get; set; }
        // имя и фамилия
        public static string Name { get; set; }

        // ник пользователя
        public static string Nickname { get; set; }

        // пароль
        public static string Password { get; set; }

        // почта
        public static string Email { get; set; }
    }
}
