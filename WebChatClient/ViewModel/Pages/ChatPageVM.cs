using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для экрана входа в систему
    /// </summary>
    public class ChatPageVM : BaseViewModel
    {
        // представление чата
        ChatPage _view;

        // коллекция моделей представления контактов
        ObservableCollection<ContactVM> _contactsVM;

        // ссылка на модель контактов
        ContactsModel _contactsModel;

        ObservableCollection<MessageVM> _messageVM;

        // индекс предыдущего выбора контакта
        int _indexOldSelected;

        // имя выбранного контакта
        public string NameSelectedContact { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля.
        public string ProfilePictureRGB { get; set; }

        // Открывает текущую ветку сообщений
        public ICommand OpenMessageCommand { get; set; }

        public ChatPageVM(ChatPage view)
        {
            _view = view;
            
            // инициализация
            _contactsVM = new ObservableCollection<ContactVM>();
            _contactsModel = new ContactsModel();
            _messageVM = new ObservableCollection<MessageVM>();

            // Создание команд
            OpenMessageCommand = new Command((o) => OpenMessage());

            // загрузка контактов
            LoadingContacts();
        }

        // открыть ветку с сообщениями
        private void OpenMessage()
        {
            int currentSelectedIndex = _view.ListContacts.SelectedIndex;

            _contactsVM[_indexOldSelected].IsSelected = false;
            _contactsVM[currentSelectedIndex].IsSelected = true;

            _indexOldSelected = _view.ListContacts.SelectedIndex;

            NameSelectedContact = _contactsVM[currentSelectedIndex].Name;

            TreeMessagesContactModel treeMessagesModel = new TreeMessagesContactModel(_contactsVM[currentSelectedIndex].UserID);

            foreach (Message message in treeMessagesModel.TreeMessagesContact)
            {
                MessageVM messageVM = new MessageVM();

                messageVM.Message = message.TextMessage;
                messageVM.MessageSentTime = message.MessageSentTime;
                messageVM.SentByMe = message.SentByMe;
                messageVM.Initials = _contactsVM[currentSelectedIndex].Initials;
                messageVM.ProfilePictureRGB = _contactsVM[currentSelectedIndex].ProfilePictureRGB;

                _messageVM.Add(messageVM);

                MessageControl messageControl = new MessageControl();
                messageControl.DataContext = messageVM;

                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = messageControl;
                _view.TreeMessages.Items.Add(lbi);
            }
        }

        /// <summary>
        /// загрузка контактов в список контактов
        /// </summary>
        public void LoadingContacts()
        {
            // связываем view-viewmodel-model
            foreach (Contact item in _contactsModel.Contacts)
            {
                // создать модель представления контакта
                ContactVM contactVM = new ContactVM();

                // связать VM c M
                contactVM.UserID = item.UserID;
                contactVM.Name = item.Name;
                contactVM.Message = item.Message;
                contactVM.Initials = item.Initials;
                contactVM.ProfilePictureRGB = item.ProfilePictureRGB;

                // добавить в коллекцию
                _contactsVM.Add(contactVM);

                // создать представление контакта
                ContactControl contactControl = new ContactControl();
                // связать пердставление с модель представления 
                contactControl.DataContext = contactVM;

                // добавить представление контакта в список контактов в чате
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = contactControl;
                _view.ListContacts.Items.Add(lbi);
            }
        }
    }
}
