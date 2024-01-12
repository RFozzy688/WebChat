using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления списка цепочек сообщений в чата
    /// </summary>
    public class ChatMessageListVM : BaseViewModel
    {
        // Элементы ветки чата для списка
        public ObservableCollection<ChatMessageListItemVM> Items { get; set; }

        // True, чтобы показать меню вложений, false, чтобы скрыть его.
        public bool AttachmentMenuVisible { get; set; }

        // Истинно, если видны всплывающие меню.
        public bool AnyPopupVisible => AttachmentMenuVisible;

        // Текст для текущего сообщения
        public string PendingMessageText { get; set; }

        // Модель представления меню вложений
        public ChatAttachmentPopupMenuVM AttachmentMenu { get; set; }

        // Команда для нажатия кнопки вложения
        public ICommand AttachmentButtonCommand { get; set; }

        // Команда для щелчка по области за пределами всплывающего окна.
        public ICommand PopupClickawayCommand { get; set; }

        // Команда, когда пользователь нажимает кнопку отправки
        public ICommand SendCommand { get; set; }

        public ChatMessageListVM()
        {
            // Создание команд
            AttachmentButtonCommand = new Command((o) => AttachmentButton());
            PopupClickawayCommand = new Command((o) => PopupClickaway());
            SendCommand = new Command((o) => Send());

            // Сделать меню по умолчанию
            AttachmentMenu = new ChatAttachmentPopupMenuVM();
        }

        // Когда пользователь нажимает кнопку отправить, отправляет сообщение
        public void Send()
        {
            if (Items == null)
                Items = new ObservableCollection<ChatMessageListItemVM>();

            // отправить фейковое новое сообщение
            Items.Add(new ChatMessageListItemVM
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Luke Malpass",
                NewItem = true
            });

            // Очистить текст ожидающего сообщения
            PendingMessageText = string.Empty;
        }

        // При нажатии на область щелчка всплывающего окна скрывают все всплывающие окна.
        private void PopupClickaway()
        {
            // Скрыть меню вложений
            AttachmentMenuVisible = false;
        }

        // При нажатии кнопки вложения показать/скрыть всплывающее окно с вложением
        public void AttachmentButton()
        {
            // Переключить видимость меню
            AttachmentMenuVisible ^= true;
        }
    }
}
