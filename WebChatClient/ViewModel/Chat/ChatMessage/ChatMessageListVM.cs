using System;
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

        // Истинно, если видны всплывающие меню.
        public bool AnyPopupVisible => AttachmentMenuVisible;

        // Модель представления меню вложений
        public ChatAttachmentPopupMenuVM AttachmentMenu { get; set; }

        // Команда для нажатия кнопки вложения
        public ICommand AttachmentButtonCommand { get; set; }

        // Команда для щелчка по области за пределами всплывающего окна.
        public ICommand PopupClickawayCommand { get; set; }

        public ChatMessageListVM()
        {
            // Создание команд
            AttachmentButtonCommand = new Command((param) => AttachmentButton(param));
            PopupClickawayCommand = new Command((param) => PopupClickaway(param));

            // Сделать меню по умолчанию
            AttachmentMenu = new ChatAttachmentPopupMenuVM();
        }

        // При нажатии на область щелчка всплывающего окна скрывают все всплывающие окна.
        private void PopupClickaway(object param)
        {
            // Скрыть меню вложений
            AttachmentMenuVisible = false;
        }

        // При нажатии кнопки вложения показать/скрыть всплывающее окно с вложением
        public void AttachmentButton(object param)
        {
            // Переключить видимость меню
            AttachmentMenuVisible ^= true;
        }
    }
}
