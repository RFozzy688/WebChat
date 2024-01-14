using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для каждого элемента ветки сообщений чата в ветке чата.
    /// </summary>
    public class ChatMessageListItemVM : BaseViewModel
    {
        // Отображаемое имя отправителя сообщения
        public string SenderName { get; set; }

        // Последнее сообщение из этого чата
        public string Message { get; set; }

        // Инициалы, которые будут отображаться в качестве фона изображения профиля
        public string Initials { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля
        public string ProfilePictureRGB { get; set; }

        // Истинно, если этот элемент выбран в данный момент
        public bool IsSelected { get; set; }

        // True, если это сообщение было отправлено вошедшим пользователем
        public bool SentByMe { get; set; }

        // Время прочтения сообщения или <see cref="DateTimeOffset.MinValue"/>, если оно не прочитано.
        public DateTimeOffset MessageReadTime { get; set; }

        // Верно, если это сообщение было прочитано
        public bool MessageRead => MessageReadTime > DateTimeOffset.MinValue;

        // Время отправки сообщения
        public DateTimeOffset MessageSentTime { get; set; }

        // Флаг, указывающий, был ли этот элемент добавлен с момента создания первого основного списка элементов.
        // Используется как флаг для анимации
        public bool NewItem { get; set; }


        // Вложение к сообщению, если оно имеет тип изображения
        public ChatMessageListItemImageAttachmentVM ImageAttachment { get; set; }

        // Флаг, указывающий, есть ли у нас текст сообщения или нет
        public bool HasMessage => Message != null;

        // Флаг, указывающий, есть ли у нас изображение, прикрепленное к этому сообщению
        public bool HasImageAttachment => ImageAttachment != null;
    }
}
