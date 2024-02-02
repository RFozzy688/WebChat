using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace WebChatClient
{
    /// <summary>
    /// контакты, модель
    /// </summary>
    public class ContactsModel
    {
        // путь к файлу с контактами
        string _path;

        // коллекция контактов
        public List<Contact>? Contacts;

        public ContactsModel() 
        {
            Contacts = new List<Contact>();

            // путь к файлу с контактами
            CreatePath();

            LoadingContacts();
            #region Contacts
            //Contacts = new List<Contact>
            //{
            //    new Contact
            //    {
            //        UserID = "836a0851-a37d-4599-8507-284bd224d8ee",
            //        Name = "Luke",
            //        Initials = "LM",
            //        Message = "This chat app is awesome! I bet it will be fast too",
            //        ProfilePictureRGB = "3099c5"
            //    },
            //    new Contact
            //    {
            //        UserID = "da966c35-8ce5-41d3-9655-5cd06d591334",
            //        Name = "Jesse",
            //        Initials = "JA",
            //        Message = "Hey dude, here are the new icons",
            //        ProfilePictureRGB = "fe4503"
            //    },
            //    new Contact
            //    {
            //        UserID = "a9367247-9440-40d4-9e24-35c6f615705c",
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "The new server is up, got 192.168.1.1",
            //        ProfilePictureRGB = "00d405"
            //    },
            //    new Contact
            //    {
            //        UserID = "ba485e79-919d-450a-8a40-a77543ef90d3",
            //        Name = "Катя",
            //        Initials = "КК",
            //        Message = "Давно выяснено, что при оценке дизайна и композиции читаемый текст мешает сосредоточиться",
            //        ProfilePictureRGB = "3099c5"
            //    },
            //    new Contact
            //    {
            //        UserID = "75464eb8-0625-4762-91ef-393b673a6008",
            //        Name = "Марк",
            //        Initials = "JA",
            //        Message = "Lorem Ipsum используют потому",
            //        ProfilePictureRGB = "fe4503"
            //    },
            //    new Contact
            //    {
            //        UserID = "7f8296ee-9870-46fe-a66f-72d53c0336f0",
            //        Name = "Вова",
            //        Initials = "ВА",
            //        Message = "Есть много вариантов Lorem Ipsum, но большинство из них имеет не всегда приемлемые модификации,",
            //        ProfilePictureRGB = "00d405"
            //    },
            //    new Contact
            //    {
            //        UserID = "5cf43fe6-7b0b-42c7-8cd0-abf4d7445597",
            //        Name = "Таня",
            //        Initials = "ТН",
            //        Message = "Если вам нужен Lorem Ipsum для серьёзного проекта, вы наверняка",
            //        ProfilePictureRGB = "3099c5"
            //    },
            //    new Contact
            //    {
            //        UserID = "cb10f581-bc33-46f9-b03d-5edc6cf24a0d",
            //        Name = "Сергей",
            //        Initials = "СВ",
            //        Message = "Ричард МакКлинток, профессор латыни из колледжа Hampden-Sydney,",
            //        ProfilePictureRGB = "fe4503"
            //    },
            //    new Contact
            //    {
            //        UserID = "b406cef3-02bf-4a84-af43-acd6fc3e5648",
            //        Name = "Ден",
            //        Initials = "ДА",
            //        Message = "Многие программы электронной вёрстки и редакторы HTML",
            //        ProfilePictureRGB = "00d405"
            //    }
            //};
            #endregion
        }

        // сохраняет новый контакт в файл
        public void SaveContact(AddUserToContactList addUser)
        {
            // создать контакт
            Contact contact = new Contact();
            contact.Name = addUser.Name;
            contact.UserID = addUser.UserID;
            contact.Initials = addUser.Name.Remove(2); // оставляем первые две буквы
            contact.ProfilePictureRGB = Guid.NewGuid().ToString().Remove(6); // случайный цвет
            contact.Message = "Нет сообщений";

            // путь к файлу с контактами
            //CreatePath();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                // если файл не пуст
                if (fs.Seek(0, SeekOrigin.End) != 0)
                {
                    // то дописываем новый контакт в коллекцию
                    fs.Seek(-1, SeekOrigin.End);
                    fs.Write(Encoding.Default.GetBytes(","));
                    JsonSerializer.Serialize(fs, contact);
                    fs.Write(Encoding.Default.GetBytes("]"));
                }
                else
                {
                    // создаем коллекцию
                    fs.Write(Encoding.Default.GetBytes("["));
                    JsonSerializer.Serialize(fs, contact);
                    fs.Write(Encoding.Default.GetBytes("]"));
                }
            }

            // добавить контакт в коллекцию
            AddContactToList(contact);
        }

        // создать путь к файлу с контактами
        private void CreatePath()
        {
            _path = Environment.CurrentDirectory;
            int index = _path.LastIndexOf("WebChatClient");
            _path = _path.Remove(index + "WebChatClient".Length);
            _path += $"\\db\\contacts.json";
        }

        // добавим контакт в коллекцию
        private void AddContactToList(Contact contact)
        {
            Contacts.Add(contact);
        }

        private void LoadingContacts()
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                Contacts = JsonSerializer.Deserialize<List<Contact>>(fs);
            }
        }
    }

    /// <summary>
    /// данные контакта
    /// </summary>
    public class Contact
    {
        // уникальный идентификатор пользователя в этом приложении
        public string UserID { get; set; }

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
