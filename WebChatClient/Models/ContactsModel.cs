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
    public static class ContactsModel
    {
        // путь к файлу с контактами
        static string _path;

        // коллекция контактов
        static public List<Contact>? Contacts;

        static ContactsModel() 
        {
            Contacts = new List<Contact>();

            // путь к файлу с контактами
            CreatePath();

            LoadingContacts();
        }

        // сохраняет новый контакт в файл
        static public void SaveContact(GeneralUserData addUser)
        {
            // создать контакт
            Contact contact = new Contact();
            contact.Name = addUser.Name;
            contact.UserID = addUser.UserID;
            contact.Initials = addUser.Name.Remove(2); // оставляем первые две буквы
            contact.ProfilePictureRGB = Guid.NewGuid().ToString().Remove(6); // случайный цвет
            contact.Message = "Нет сообщений";
            contact.NewContentAvailable = false;

            // путь к файлу с контактами
            //CreatePath();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                // если файл не пуст
                if (fs.Length != 0)
                {
                    // то дописываем новый контакт в коллекцию
                    fs.Seek(-1, SeekOrigin.End);
                    fs.Write(Encoding.UTF8.GetBytes(","));
                    JsonSerializer.Serialize(fs, contact);
                    fs.Write(Encoding.UTF8.GetBytes("]"));
                }
                else
                {
                    // создаем коллекцию
                    fs.Write(Encoding.UTF8.GetBytes("["));
                    JsonSerializer.Serialize(fs, contact);
                    fs.Write(Encoding.UTF8.GetBytes("]"));
                }
            }

            // добавить контакт в коллекцию
            AddContactToList(contact);
            // создать файл истории для нового пользователя
            CreateUserStory(contact.UserID);
        }

        // создать путь к файлу с контактами
        static private void CreatePath()
        {
            _path = Environment.CurrentDirectory;
            int index = _path.LastIndexOf("WebChatClient");
            _path = _path.Remove(index + "WebChatClient".Length);
            _path += @"\db";

            // если каталога "db" не существует
            if (!Directory.Exists(_path))
            {
                // создаем каталог
                Directory.CreateDirectory(_path);
            }

            _path += @$"\{DetailsProfileModel.UserID}.json";
        }

        // добавим контакт в коллекцию
        static private void AddContactToList(Contact contact)
        {
            Contacts.Add(contact);
        }

        // загрузка контактов из файла
        static private void LoadingContacts()
        {
            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                if(fs.Length != 0)
                {
                    Contacts = JsonSerializer.Deserialize<List<Contact>>(fs);
                }
            }
        }

        // сохранить все контакты в файл
        static public void SaveAllContacts()
        {
            // путь к файлу с контактами
            CreatePath();

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                // сохранение контактов
                JsonSerializer.Serialize(fs, Contacts);
            }
        }

        // создать файл истории для нового пользователя
        static private void CreateUserStory(string userID)
        {
            string path = Environment.CurrentDirectory;
            int index = path.LastIndexOf("WebChatClient");
            path = path.Remove(index + "WebChatClient".Length);
            path += @"\db\UsersStories";

            // если каталога "UsersStories" не существует
            if (!Directory.Exists(path))
            {
                // создаем каталог
                Directory.CreateDirectory(path);
            }

            path += $"\\{userID}.json";

            // создать файл истории
            File.Create(path);
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

        // Верно, если в этом чате есть непрочитанные сообщения.
        public bool NewContentAvailable { get; set; }
    }
}
