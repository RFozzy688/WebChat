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
        private void Send()
        {
            IoC.UI.ShowMessage(new MessageBoxDialogVM
            {
                Title = "Send Message",
                Message = "Thank you for writing a nice message :)",
                OkText = "OK"
            });
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
