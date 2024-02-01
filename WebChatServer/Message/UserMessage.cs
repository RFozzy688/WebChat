
namespace WebChatServer
{
    // сообщение отправленное пользователем
    public class UserMessage
    {
        // от кого
        public string UserId { get; set; }

        // кому
        public string RecipientId { get; set; }

        // сообщение
        public string Message { get; set; }
    }
}
