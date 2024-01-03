using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WebChatClient
{
    public class ChatListItemDesignModel : ChatListItemVM
    {
        /// <summary>
        /// Один экземпляр проектной модели
        /// </summary>
        public static ChatListItemDesignModel Instance => new ChatListItemDesignModel();

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ChatListItemDesignModel()
        {
            Initials = "RF";
            Name = "Fozzy";
            Message = "Приложение для общения в интернете с друзьями";
            ProfilePictureRGB = "3099c5";
        }
    }
}
