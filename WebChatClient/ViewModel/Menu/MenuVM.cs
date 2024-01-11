using System.Collections.Generic;
using WebChatCore;

namespace WebChatClient
{
    // Модель представления для меню
    public class MenuVM : BaseViewModel
    {
        // Элементы этого меню
        public List<MenuItemVM> Items { get; set; }
    }
}
