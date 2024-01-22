using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebChatClient
{
    /// <summary>
    /// сообщения, модель
    /// </summary>
    public class TreeMessagesContactModel
    {
        string _userID;
        string _path;

        public List<Message> TreeMessagesContact;

        public TreeMessagesContactModel(string userID)
        {
            _userID = userID;

            TreeMessagesContact = new List<Message>();

            CreatePath();
            LoadingTreeMessagesContact();
        }

        void CreatePath()
        {
            _path = Environment.CurrentDirectory;
            int index = _path.LastIndexOf("WebChatClient");
            _path = _path.Remove(index + "WebChatClient".Length);
            _path += $"\\UsersStories\\{_userID}.json";
        }

        void LoadingTreeMessagesContact()
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                TreeMessagesContact = JsonSerializer.Deserialize<List<Message>>(fs);
            }
        }
    }

    public class Message
    {
        // Сообщение из этого чата
        public string TextMessage { get; set; }

        // Локальный путь к загруженному миниатюре на этом компьютере.
        public string LocalFilePath { get; set; }

        // True, если это сообщение было отправлено вошедшим пользователем
        public bool SentByMe { get; set; }

        // Время отправки сообщения
        public DateTimeOffset MessageSentTime { get; set; }
    }
}
