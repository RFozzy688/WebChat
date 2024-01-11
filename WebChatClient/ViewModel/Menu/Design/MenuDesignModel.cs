using System.Collections.Generic;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Данные времени разработки для <see cref="MenuViewModel"/>
    /// </summary>
    public class MenuDesignModel : MenuVM
    {
        // Один экземпляр проектной модели
        public static MenuDesignModel Instance => new MenuDesignModel();

        public MenuDesignModel()
        {
            Items = new List<MenuItemVM>(new[]
            {
                new MenuItemVM { Type = MenuItemType.Header, Text = "Design time header..." },
                new MenuItemVM { Text = "Menu item 1", Icon = IconType.File },
                new MenuItemVM { Text = "Menu item 2", Icon = IconType.Picture },
            });
        }
    }
}
