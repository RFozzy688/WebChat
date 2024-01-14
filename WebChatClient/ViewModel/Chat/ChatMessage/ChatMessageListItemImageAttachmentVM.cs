using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebChatClient
{
    // Модель представления для каждого вложения элемента цепочки сообщений чата
    // (в данном случае изображение) в ветке чата
    public class ChatMessageListItemImageAttachmentVM : BaseViewModel
    {
        // URL миниатюры этого вложения
        private string _thumbnailUrl;

        // Название этого файла изображения
        public string Title { get; set; }

        // Исходное имя файла вложения
        public string FileName { get; set; }

        // Размер файла этого вложения в байтах
        public long FileSize { get; set; }

        // URL миниатюры этого вложения
        public string ThumbnailUrl
        {
            get => _thumbnailUrl;
            set
            {
                // Если значение не изменилось, return
                if (value == _thumbnailUrl)
                    return;

                // Обновить значение
                _thumbnailUrl = value;

                // TODO: Скачать изображение с сайта
                // Сохраняем файл в локальном хранилище/кэше
                // Устанавливаем значение пути к локальному файлу
                //
                //       А пока просто установите путь к файлу напрямую
                Task.Delay(2000).ContinueWith(t => LocalFilePath = "/Images/Samples/p2.jpg");
            }
        }

        // Локальный путь к загруженному миниатюре на этом компьютере.
        public string LocalFilePath { get; set; }

        // Указывает, загружено ли изображение
        public bool ImageLoaded => LocalFilePath != null;
    }
}

