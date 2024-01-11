using WebChatCore;

namespace WebChatClient
{
    public class MenuItemVM : BaseViewModel
    {
        // Текст, отображаемый для пункта меню
        public string Text { get; set; }

        // Значок этого пункта меню
        public IconType Icon { get; set; }

        // Тип этого пункта меню
        public MenuItemType Type { get; set; }
    }
}
