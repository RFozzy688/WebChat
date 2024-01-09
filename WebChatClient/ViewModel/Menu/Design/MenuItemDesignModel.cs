using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Данные времени разработки для <see cref="MenuItemViewModel"/>
    /// </summary>
    public class MenuItemDesignModel : MenuItemVM
    {
        // Один экземпляр проектной модели
        public static MenuItemDesignModel Instance => new MenuItemDesignModel();

        public MenuItemDesignModel()
        {
            Text = "Hello World";
            Icon = IconType.File;
        }
    }
}
