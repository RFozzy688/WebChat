

using System.Collections.ObjectModel;

namespace WebChatClient
{
    /// <summary>
    /// контакты, модель
    /// </summary>
    public class ContactsModel
    {
        public ObservableCollection<Contact> Contacts;

        public ContactsModel() 
        {
            Contacts = new ObservableCollection<Contact>
            {
                new Contact
                {
                    Name = "Luke",
                    Initials = "LM",
                    Message = "This chat app is awesome! I bet it will be fast too",
                    ProfilePictureRGB = "3099c5"
                },
                new Contact
                {
                    Name = "Jesse",
                    Initials = "JA",
                    Message = "Hey dude, here are the new icons",
                    ProfilePictureRGB = "fe4503"
                },
                new Contact
                {
                    Name = "Parnell",
                    Initials = "PL",
                    Message = "The new server is up, got 192.168.1.1",
                    ProfilePictureRGB = "00d405"
                },
                new Contact
                {
                    Name = "Luke",
                    Initials = "LM",
                    Message = "This chat app is awesome! I bet it will be fast too",
                    ProfilePictureRGB = "3099c5"
                },
                new Contact
                {
                    Name = "Jesse",
                    Initials = "JA",
                    Message = "Hey dude, here are the new icons",
                    ProfilePictureRGB = "fe4503"
                },
                new Contact
                {
                    Name = "Parnell",
                    Initials = "PL",
                    Message = "The new server is up, got 192.168.1.1",
                    ProfilePictureRGB = "00d405"
                },
                new Contact
                {
                    Name = "Luke",
                    Initials = "LM",
                    Message = "This chat app is awesome! I bet it will be fast too",
                    ProfilePictureRGB = "3099c5"
                },
                new Contact
                {
                    Name = "Jesse",
                    Initials = "JA",
                    Message = "Hey dude, here are the new icons",
                    ProfilePictureRGB = "fe4503"
                },
                new Contact
                {
                    Name = "Parnell",
                    Initials = "PL",
                    Message = "The new server is up, got 192.168.1.1",
                    ProfilePictureRGB = "00d405"
                }
            };
        }
    }

    /// <summary>
    /// данные контакта
    /// </summary>
    public class Contact
    {
        // Отображаемое имя в списке чатов
        public string Name { get; set; }

        // Последнее сообщение из этого чата
        public string Message { get; set; }

        // Инициалы, которые будут отображаться в качестве фона изображения профиля.
        public string Initials { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля.
        public string ProfilePictureRGB { get; set; }

        #region MyRegion
        // Верно, если в этом чате есть непрочитанные сообщения.
        //public bool NewContentAvailable { get; set; }

        // Истинно, если этот элемент выбран в данный момент
        //public bool IsSelected { get; set; } 
        #endregion
    }
}
