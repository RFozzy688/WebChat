
namespace WebChatServer
{
    // сообщение отправленное пользователем
    public class UserMessage
    {
        // от кого
        public string IdUser { get; set; }

        // кому
        public string IdRecipient { get; set; }

        // сообщение
        public string Message { get; set; }
    }
}
