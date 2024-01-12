using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для каждого элемента списка чатов в обзорном списке чатов.
    /// </summary>
    public class ChatListItemVM : BaseViewModel
    {
        // Отображаемое имя в списке чатов
        public string Name { get; set; }

        // Последнее сообщение из этого чата
        public string Message { get; set; }

        // Инициалы, которые будут отображаться в качестве фона изображения профиля.
        public string Initials { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля.
        public string ProfilePictureRGB { get; set; }

        // Верно, если в этом чате есть непрочитанные сообщения.
        public bool NewContentAvailable { get; set; }

        // Истинно, если этот элемент выбран в данный момент
        public bool IsSelected { get; set; }

        // Открывает текущую ветку сообщений
        public ICommand OpenMessageCommand { get; set; }

        // Конструктор по умолчанию
        public ChatListItemVM()
        {
            // Создание команд
            OpenMessageCommand = new Command((o) => OpenMessage());
        }

        public void OpenMessage()
        {
            //if (Name == "Jesse")
            //{
            //    ViewModelApplication.GoToPage(ApplicationPage.Login, new LoginViewModel
            //    {
            //        Email = "jesse@helloworld.com"
            //    });
            //    return;
            //}

            IoC.Application.GoToPage(AppPage.Chat, new ChatMessageListVM
            {
                //DisplayTitle = "Parnell, Me",

                Items = new ObservableCollection<ChatMessageListItemVM>
                {
                    new ChatMessageListItemVM
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF00FF",
                        SenderName = "Luke",
                        SentByMe = true,
                    },
                    new ChatMessageListItemVM
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemVM
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemVM
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF00FF",
                        SenderName = "Luke",
                        SentByMe = true,
                    },
                    new ChatMessageListItemVM
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemVM
                    {
                        Message = "A received message",
                        ImageAttachment = new ChatMessageListItemImageAttachmentVM
                        {
                            ThumbnailUrl = "http://anywhere"
                        },
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                }
            });
        }
    }
}
