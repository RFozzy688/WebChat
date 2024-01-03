using System.Collections.Generic;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления обзорного списка чата.
    /// </summary>
    public class ChatListVM : BaseViewModel
    {
        /// <summary>
        /// Список чата
        /// </summary>
        public List<ChatListItemVM> Items { get; set; }
    }
}
