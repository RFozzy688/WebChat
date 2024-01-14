using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatCore;

namespace WebChatClient
{
    public class ChatAttachmentPopupMenuVM : BasePopupVM
    {
        public ChatAttachmentPopupMenuVM() 
        {
            Content = new MenuVM
            {
                Items = new ObservableCollection<MenuItemVM>(new[]
                {
                    new MenuItemVM { Text = "Attach a file...", Type = MenuItemType.Header },
                    new MenuItemVM { Text = "From Computer", Icon = IconType.File },
                    new MenuItemVM { Text = "From Pictures", Icon = IconType.Picture },
                })
            };
        }
    }
}
