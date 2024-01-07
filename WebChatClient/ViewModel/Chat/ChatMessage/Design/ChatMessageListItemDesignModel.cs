using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WebChatClient
{
    public class ChatMessageListItemDesignModel : ChatMessageListItemVM
    {
        /// <summary>
        /// Один экземпляр проектной модели
        /// </summary>
        public static ChatMessageListItemDesignModel Instance => new ChatMessageListItemDesignModel();

        public ChatMessageListItemDesignModel()
        {
            Initials = "RF";
            SenderName = "Fozzy";
            Message = "Some design time visual text";
            ProfilePictureRGB = "3099c5";
            SentByMe = true;
            MessageSentTime = DateTimeOffset.UtcNow;
            MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3));
        }
    }
}
