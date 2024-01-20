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

        // ссылка на коллекцию моделей представления дерева сообщений
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

            // при первом открытии приложения
            _indexOldSelected = -1; 

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
            // индекс текущего выделенного елемента
            int currentSelectedIndex = _view.ListContacts.SelectedIndex;

            if (currentSelectedIndex == _indexOldSelected)
            {
                return;
            }

            if (_indexOldSelected != -1)
            {
                // снять выделение с предыдущего елемента
                _contactsVM[_indexOldSelected].IsSelected = false;
            }
           
            // выделить текущий
            _contactsVM[currentSelectedIndex].IsSelected = true;

            // сохраняем индекс текущего выделенного елемента
            _indexOldSelected = _view.ListContacts.SelectedIndex;

            // имя контакта в заголовке дерева сообщений
            NameSelectedContact = _contactsVM[currentSelectedIndex].Name;

            _view.TreeMessages.Items.Clear();

            // создать модель дерева сообщений
            // передать id выделенного контакта и загрузить сообщения в модель
            TreeMessagesContactModel treeMessagesModel = new TreeMessagesContactModel(_contactsVM[currentSelectedIndex].UserID);

            int i = 0;
            foreach (Message message in treeMessagesModel.TreeMessagesContact)
            {
                // создать модель представления сообщения
                MessageVM messageVM = new MessageVM();

                // проинициализировать VM из M
                messageVM.Message = message.TextMessage;
                messageVM.MessageSentTime = message.MessageSentTime;
                messageVM.SentByMe = message.SentByMe;
                // инициализация VM из VM контакта
                messageVM.Initials = _contactsVM[currentSelectedIndex].Initials;
                messageVM.ProfilePictureRGB = _contactsVM[currentSelectedIndex].ProfilePictureRGB;

                // добавить в коллекцию VM
                _messageVM.Add(messageVM);

                // создать пузырь сообщения
                MessageControl messageControl = new MessageControl();
                // связать V с VM
                messageControl.DataContext = messageVM;

                // добавить сообщение в ListBox
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = messageControl;
                _view.TreeMessages.Items.Add(lbi);i++;
            }

            // прокрутить ListBox в конец
            _view.TreeMessages.ScrollIntoView(_view.TreeMessages.Items[_view.TreeMessages.Items.Count - 2]);
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
