using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        public ObservableCollection<ChatListItemVM> Items { get; set; }
    }
}
