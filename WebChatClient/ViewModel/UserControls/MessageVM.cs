using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatClient
{
    // Модель представления для каждого сообщения в ветке сообщений
    public class MessageVM : BaseViewModel
    {
        // Отображаемое имя отправителя сообщения
        public string SenderName { get; set; }

        // Сообщение из этого чата
        public string Message { get; set; }

        // Инициалы, которые будут отображаться в качестве фона изображения профиля
        public string Initials { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля
        public string ProfilePictureRGB { get; set; }

        // Истинно, если этот элемент выбран в данный момент
        public bool IsSelected { get; set; }

        // True, если это сообщение было отправлено вошедшим пользователем
        public bool SentByMe { get; set; }

        // Верно, если это сообщение было прочитано
        //public bool MessageRead {  get; set; }

        // Время отправки сообщения
        public DateTimeOffset MessageSentTime { get; set; }
    }
}
