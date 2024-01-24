using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;

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

        // Элементы ветки чата для списка, включающие любые поисковые фильтры
        ObservableCollection<MessageVM> _filteredMessages;

        // индекс предыдущего выбора контакта
        int _indexOldSelected;

        // Флаг, указывающий, открыт ли диалог поиска
        bool _searchIsOpen;

        // Последний искомый текст в этом списке
        string _lastSearchText;

        // Текст для поиска в команде поиска
        string _searchText;

        // имя выбранного контакта
        public string NameSelectedContact { get; set; }

        // Значения RGB (в шестнадцатеричном формате) для цвета фона изображения профиля.
        public string ProfilePictureRGB { get; set; }

        // Флаг, указывающий, открыт ли диалог поиска
        public bool SearchIsOpen { get; set; }

        // Текст для поиска, когда мы выполняем поиск
        public string SearchText { get; set; }

        // Текст для текущего сообщения
        public string PendingMessageText { get; set; }

        // True, если меню настроек должно отображаться
        public Visibility SettingsMenuVisible { get; set; } = Visibility.Collapsed;

        // Открывает текущую ветку сообщений
        public ICommand OpenMessageCommand { get; set; }

        // Команда, когда пользователь хочет открыть диалоговое окно поиска
        public ICommand OpenSearchCommand { get; set; }

        // Команда, когда пользователь хочет закрыть диалоговое окно поиска
        public ICommand CloseSearchCommand { get; set; }

        // Команда, когда пользователь хочет очистить текст поиска
        public ICommand ClearSearchCommand { get; set; }

        // Команда, когда пользователь хочет выполнить поиск
        public ICommand SearchCommand { get; set; }

        // Команда, когда пользователь нажимает кнопку отправки
        public ICommand SendCommand { get; set; }

        // Команда, когда пользователь хочет открыть окно настроек
        public ICommand OpenSettingsCommand { get; set; }

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
            OpenSearchCommand = new Command((o) => OpenSearch());
            CloseSearchCommand = new Command((o) => CloseSearch());
            SearchCommand = new Command((o) => Search());
            ClearSearchCommand = new Command((o) => ClearSearch());
            SendCommand = new Command((o) => Send());
            OpenSettingsCommand = new Command((o) => OpenSettings());

            // загрузка контактов
            LoadingContacts();
        }

        private void OpenSettings()
        {
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new SettingsPage(_view);
        }

        // Когда пользователь нажимает кнопку отправить, отправляет сообщение
        public void Send()
        {
            // Не отправляйте пустое сообщение
            if (string.IsNullOrEmpty(PendingMessageText))
                return;

            // Убедитесь, что списки не равны нулю
            if (_messageVM == null)
            {
                _messageVM = new ObservableCollection<MessageVM>();
            }

            // отправить фейковое новое сообщение
            var message = new MessageVM
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                ProfilePictureRGB = "000000"
            };

            // Добавить сообщение в список
            _messageVM.Add(message);

            // создание и привязка VM
            MessageControl messageControl = new MessageControl();
            messageControl.DataContext = message;

            // добавить представление в ListBox
            AddToListBox(_view.TreeMessages, messageControl);

            // прокрутить ListBox в конец
            _view.TreeMessages.ScrollIntoView(_view.TreeMessages.Items[_view.TreeMessages.Items.Count - 1]);

            // Очистить текст ожидающего сообщения
            PendingMessageText = string.Empty;
        }

        // Открывает диалог поиска
        public void OpenSearch() => SearchIsOpen = true;

        // Закрывает диалог поиска
        public void CloseSearch()
        {
            SearchIsOpen = false;
            SearchText = string.Empty;
            _lastSearchText = string.Empty;

            // очистить дерево от предыдущих сообщений
            _view.TreeMessages.Items.Clear();

            foreach (var item in _messageVM)
            {
                // создать пузырь сообщения
                MessageControl messageControl = new MessageControl();
                // связать V с VM
                messageControl.DataContext = item;

                // добавить сообщение в ListBox
                AddToListBox(_view.TreeMessages, messageControl);
            }
        }

        // Выполняет поиск в текущем списке сообщений и фильтрует представление
        public void Search()
        {
            if (_messageVM.Count == 0) { return; }

            // Убедитесь, что мы не ищем повторно один и тот же текст
            if ((string.IsNullOrEmpty(_lastSearchText) && string.IsNullOrEmpty(SearchText)) ||
                string.Equals(_lastSearchText, SearchText))
                return;

            // Найти все элементы, содержащие заданный текст
            _filteredMessages = new ObservableCollection<MessageVM>(
                _messageVM.Where(item => item.Message.ToLower().Contains(SearchText)));

            // очистить дерево от предыдущих сообщений
            _view.TreeMessages.Items.Clear();

            foreach (var item in _filteredMessages)
            {
                // создать пузырь сообщения
                MessageControl messageControl = new MessageControl();
                // связать V с VM
                messageControl.DataContext = item;

                // добавить сообщение в ListBox
                AddToListBox(_view.TreeMessages, messageControl);
            }

            // Установить последний текст поиска
            _lastSearchText = SearchText;
        }

        // Очищает текст поиска
        public void ClearSearch()
        {
            // Если есть текст для поиска...
            if (!string.IsNullOrEmpty(SearchText))
            {
                // Очистить текст
                SearchText = string.Empty;
            }
            else
            {
                // Закрыть диалог поиска
                //SearchIsOpen = false;
                CloseSearch();
            }
        }

        // открыть ветку с сообщениями
        private void OpenMessage()
        {
            // индекс текущего выделенного елемента
            int currentSelectedIndex = _view.ListContacts.SelectedIndex;

            // выход если кликаем на выделенный контакт
            if (currentSelectedIndex == _indexOldSelected) { return; }

            // при каждом открытии дерева сообщения обнуляем колекцию VM 
            _messageVM.Clear();

            // при первом открытии приложения выделенного контакта еще нет
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

            // очистить дерево сообщений от предыдущего контакта
            _view.TreeMessages.Items.Clear();

            // создать модель дерева сообщений
            // передать id выделенного контакта и загрузить сообщения в модель
            TreeMessagesContactModel treeMessagesModel = new TreeMessagesContactModel(_contactsVM[currentSelectedIndex].UserID);

            foreach (Message message in treeMessagesModel.TreeMessagesContact)
            {
                // создать модель представления сообщения
                MessageVM messageVM = new MessageVM();

                // проинициализировать VM из M
                messageVM.Message = message.TextMessage;
                messageVM.MessageSentTime = message.MessageSentTime;
                messageVM.SentByMe = message.SentByMe;
                messageVM.LocalFilePath = message.LocalFilePath;
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
                AddToListBox(_view.TreeMessages, messageControl);
            }

            // прокрутить ListBox в конец
            _view.TreeMessages.ScrollIntoView(_view.TreeMessages.Items[_view.TreeMessages.Items.Count - 2]);
        }

        // загрузка контактов в список контактов
        private void LoadingContacts()
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
                AddToListBox(_view.ListContacts, contactControl);
            }

            // при первом открытии приложения загружаем сообщения первого контакта в списке
            if (_contactsVM.Count > 0)
            {
                _contactsVM[0].IsSelected = true;
                _view.ListContacts.SelectedIndex = 0;
                OpenMessage();
            }
        }

        /// <summary>
        /// добавить представление в ListBox
        /// </summary>
        /// <param name="listBox">список представлений</param>
        /// <param name="view">представления</param>
        private void AddToListBox(ListBox listBox, UserControl view)
        {
            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = view;
            listBox.Items.Add(lbi);
        }
    }
}
