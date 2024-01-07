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
    /// Модель представления для каждого элемента списка чатов в обзорном списке чатов.
    /// </summary>
    public class ChatListItemVM : BaseViewModel
    {
        /// <summary>
        /// Отображаемое имя в списке чатов
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Последнее сообщение из этого чата
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Инициалы, которые будут отображаться в качестве фона изображения профиля.
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля.
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// Верно, если в этом чате есть непрочитанные сообщения.
        /// </summary>
        public bool NewContentAvailable { get; set; }

        /// <summary>
        /// Истинно, если этот элемент выбран в данный момент
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Открывает текущую ветку сообщений
        /// </summary>
        //public ICommand OpenMessageCommand { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        //public ChatListItemVM()
        //{
        //    // Create commands
        //    OpenMessageCommand = new Command((parameter) => OpenMessage(parameter));
        //}

        //public void OpenMessage(object parameter)
        //{
        //    //if (Name == "Jesse")
        //    //{
        //    //    ViewModelApplication.GoToPage(ApplicationPage.Login, new LoginViewModel
        //    //    {
        //    //        Email = "jesse@helloworld.com"
        //    //    });
        //    //    return;
        //    //}

        //    //ViewModelApplication.GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel
        //    //{
        //    //    DisplayTitle = "Parnell, Me",

        //    //    Items = new ObservableCollection<ChatMessageListItemViewModel>
        //    //    {
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = Message,
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF00FF",
        //    //            SenderName = "Luke",
        //    //            SentByMe = true,
        //    //        },
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = "A received message",
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF0000",
        //    //            SenderName = "Parnell",
        //    //            SentByMe = false,
        //    //        },
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = "A received message",
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF0000",
        //    //            SenderName = "Parnell",
        //    //            SentByMe = false,
        //    //        },
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = Message,
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF00FF",
        //    //            SenderName = "Luke",
        //    //            SentByMe = true,
        //    //        },
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = "A received message",
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF0000",
        //    //            SenderName = "Parnell",
        //    //            SentByMe = false,
        //    //        },
        //    //        new ChatMessageListItemViewModel
        //    //        {
        //    //            Message = "A received message",
        //    //            ImageAttachment = new ChatMessageListItemImageAttachmentViewModel
        //    //            {
        //    //                ThumbnailUrl = "http://anywhere"
        //    //            },
        //    //            Initials = Initials,
        //    //            MessageSentTime = DateTime.UtcNow,
        //    //            ProfilePictureRGB = "FF0000",
        //    //            SenderName = "Parnell",
        //    //            SentByMe = false,
        //    //        },
        //    //    }
        //    //});
        //}
    }
}
