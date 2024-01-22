using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// модель представления контактов
    /// </summary>
    public class ContactVM : BaseViewModel
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

        // Истинно, если этот элемент выбран в данный момент
        public bool IsSelected { get; set; }

        public ContactVM()
        {
        }

    }
}
