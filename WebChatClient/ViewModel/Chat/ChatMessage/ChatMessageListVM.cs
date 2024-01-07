using System.Collections.Generic;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления списка цепочек сообщений в чата
    /// </summary>
    public class ChatMessageListVM : BaseViewModel
    {
        // Элементы ветки чата для списка
        public List<ChatMessageListItemVM> Items { get; set; }
    }
}
