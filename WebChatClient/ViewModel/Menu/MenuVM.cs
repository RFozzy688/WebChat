using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebChatCore;

namespace WebChatClient
{
    // Модель представления для меню
    public class MenuVM : BaseViewModel
    {
        // Элементы этого меню
        public ObservableCollection<MenuItemVM> Items { get; set; }
    }
}
