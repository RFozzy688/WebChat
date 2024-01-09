using System.Collections.Generic;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления списка цепочек сообщений в чата
    /// </summary>
    public class ChatMessageListVM : BaseViewModel
    {
        // Элементы ветки чата для списка
        public List<ChatMessageListItemVM> Items { get; set; }

        // True, чтобы показать меню вложений, false, чтобы скрыть его.
        public bool AttachmentMenuVisible { get; set; }

        // Модель представления меню вложений
        public ChatAttachmentPopupMenuVM AttachmentMenu { get; set; }

        // Команда для нажатия кнопки вложения
        public ICommand AttachmentButtonCommand { get; set; }

        public ChatMessageListVM()
        {
            // Создание команд
            AttachmentButtonCommand = new Command((param) => AttachmentButton(param));

            // Сделать меню по умолчанию
            AttachmentMenu = new ChatAttachmentPopupMenuVM();
        }

        // При нажатии кнопки вложения показать/скрыть всплывающее окно с вложением
        public void AttachmentButton(object param)
        {
            // Переключить видимость меню
            AttachmentMenuVisible ^= true;
        }
    }
}
