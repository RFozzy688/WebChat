using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления страницы чата
    /// </summary>
    public class ChatPageVM : BaseViewModel
    {
        // представление чата
        ChatPage _view;

        // коллекция моделей представления контактов
        ObservableCollection<ContactVM> _contactsVM;

        // ссылка на модель контактов
        //ContactsModel _contactsModel;

        // ссылка на коллекцию моделей представления дерева сообщений
        ObservableCollection<MessageVM> _messageVM;

        // Элементы ветки чата для списка, включающие любые поисковые фильтры
        ObservableCollection<MessageVM> _filteredMessages;

        // индекс предыдущего выбора контакта
        int _indexOldSelected;

        // индекс текущего выделенного елемента
        int _currentSelectedIndex;

        // идентификатор выбранного пользователя
        string _idSelectedUser;

        // Флаг, указывающий, открыт ли диалог поиска
        //bool _searchIsOpen;

        // Последний искомый текст в этом списке
        string _lastSearchText;

        // Текст для поиска в команде поиска
        //string _searchText;

        // Время отправки сообщения
        DateTimeOffset _messageSentTime;

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

        // Флаг, указывающий, открыт ли диалог добавления пользователя
        public bool AddUserIsOpen { get; set; }

        // Email пользователя для поиска в бд и добавления в контакты
        public string EmailUserText { get; set; }

        // флаг указывающий выполнение добавления пользователя в контакты 
        public bool AddUserIsRunning { get; set; }

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

        // Команда открывающая строку для поиска и добавления пользователя в контакты
        public ICommand OpenAddUserCommand { get; set; }

        // Команда закрывающая диалог для добавления пользователя в контакты
        public ICommand CloseAddUserCommand { get; set; }

        // Команда, когда пользователь хочет очистить текст поиска
        public ICommand ClearAddUserCommand { get; set; }

        // Команда, отправить email на сервер для поиска пользователя в бд
        public ICommand SendEmailUserCommand { get; set; }

        // команда для прикрепления файла
        public ICommand AttachmentButtonCommand { get; set; }

        public ChatPageVM(ChatPage view)
        {
            _view = view;

            // при первом открытии приложения
            _indexOldSelected = -1; 

            // инициализация
            _contactsVM = new ObservableCollection<ContactVM>();
            //_contactsModel = new ContactsModel();
            _messageVM = new ObservableCollection<MessageVM>();

            // Создание команд
            OpenMessageCommand = new Command((o) => OpenMessage());
            OpenSearchCommand = new Command((o) => OpenSearch());
            CloseSearchCommand = new Command((o) => CloseSearch());
            SearchCommand = new Command((o) => Search());
            ClearSearchCommand = new Command((o) => ClearSearch());
            SendCommand = new Command((o) => Send());
            OpenSettingsCommand = new Command((o) => OpenSettings());
            OpenAddUserCommand = new Command((o) => OpenAddUser());
            CloseAddUserCommand = new Command((o) => CloseAddUser());
            ClearAddUserCommand = new Command((o) => ClearAddUser());
            SendEmailUserCommand = new Command(async (o) => await SendEmailUserAsync());

            // загрузка контактов
            LoadingContacts();

            WorkWithServer.ResponceEvent += WaitingIncomingMessage;
        }

        // отправить email на сервер для поиска пользователя в бд
        private async Task SendEmailUserAsync()
        {
            if (AddUserIsRunning)
            {
                return;
            }

            // добавить себя в список контактов не возможно
            if (EmailUserText.CompareTo(DetailsProfileModel.Email) == 0)
            {
                MessageBoxModel.Title = "Ошибка";
                MessageBoxModel.Message = "Добавить себя в контакты не возможно!";

                DialogMessageBox dialog = new DialogMessageBox();
                dialog.ShowDialog();

                return;
            }

            // флаг указывающий выполнение добавления пользователя в контакты 
            AddUserIsRunning = true;

            // получить данные с TextBox
            GeneralUserData addUser = new GeneralUserData();
            addUser.Email = EmailUserText;

            // сформировать данные для отправки на сервер
            DataPackage package = new DataPackage();
            // тип пакета
            package.Package = TypeData.FindUser;
            // основные данные пакета
            package.StringSerialize = JsonSerializer.Serialize(addUser);

            // подписаться на событие о приходе ответа с сервера
            WorkWithServer.ResponceEvent += AddUserResponce;
            // отправить данные на сервер
            await WorkWithServer.SendMessageAsync(JsonSerializer.Serialize(package));
        }

        // метод вызывается по событию от сервера
        private void AddUserResponce(string str)
        {
            GeneralUserData? addUser = JsonSerializer.Deserialize<GeneralUserData>(str);

            // истина если пользователь для добавления существует
            if (addUser != null)
            {
                // истина если пользователь не находится в списке контактов
                if (_contactsVM.FirstOrDefault(o => o.UserID.CompareTo(addUser.UserID) == 0) == null)
                {

                    // сохраняем новый контакт в моделе контактов
                    ContactsModel.SaveContact(addUser);

                    // обновляе коллекцию контактов
                    CreateContactVM(ContactsModel.Contacts.Last());

                    MessageBoxModel.Message = $"Пользователь {addUser.Name} добавлен в список контактов";
                }
                else
                {
                    MessageBoxModel.Message = $"Пользователь {addUser.Name} уже добавлен";
                }
            }
            else
            {
                MessageBoxModel.Message = "Пользователь не найден";
            }

            MessageBoxModel.Title = "Сообщение";

            DialogMessageBox dialog = new DialogMessageBox();
            dialog.ShowDialog();

            // отписаться
            WorkWithServer.ResponceEvent -= AddUserResponce;

            // метод SendEmailUserAsync() завершил работу
            AddUserIsRunning = false;
        }

        // очистить/закрыть строку поиска пользователя
        private void ClearAddUser()
        {
            if (!string.IsNullOrEmpty(EmailUserText))
            {
                EmailUserText = string.Empty;
            }
            else
            {
                CloseAddUser();
            }
        }

        // Команда закрывающая диалог для добавления пользователя в контакты
        private void CloseAddUser()
        {
            AddUserIsOpen = false;
            EmailUserText = string.Empty;
        }

        // Откравает строку для поиска пользователя в бд и добавления в контакты
        private void OpenAddUser() => AddUserIsOpen = true;

        // открыть страницу с настройками
        private void OpenSettings()
        {
            ((MainWindowVM)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = new SettingsPage(_view);
        }

        // Когда пользователь нажимает кнопку отправить, отправляет сообщение
        public async void Send()
        {
            // Не отправляйте пустое сообщение
            if (string.IsNullOrEmpty(PendingMessageText))
                return;

            // Убедитесь, что списки не равны нулю
            if (_messageVM == null)
            {
                _messageVM = new ObservableCollection<MessageVM>();
            }

            // Время отправки сообщения
            _messageSentTime = DateTime.Now;

            // отправка сообщения на сервер
            await SendMessageToServerAsync();

            // создать VM сообщения для добавления в дерево сообщений
            var message = new MessageVM
            {
                TextMessage = PendingMessageText,
                MessageSentTime = _messageSentTime,
                SentByMe = true,
                LocalFilePath = string.Empty,
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

            // сохранить сообщение в истории
            AddMessageToStory(_idSelectedUser, message);

            _contactsVM[_currentSelectedIndex].Message = PendingMessageText;
            ContactsModel.Contacts![_currentSelectedIndex].Message = PendingMessageText;

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
                _messageVM.Where(item => item.TextMessage.ToLower().Contains(SearchText)));

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
                CloseSearch();
            }
        }

        // открыть ветку с сообщениями
        private void OpenMessage()
        {
            // индекс текущего выделенного елемента
            _currentSelectedIndex = _view.ListContacts.SelectedIndex;

            // выход если кликаем на выделенный контакт
            if (_currentSelectedIndex == _indexOldSelected) { return; }

            // при каждом открытии дерева сообщения обнуляем колекцию VM 
            _messageVM.Clear();

            // при первом открытии приложения выделенного контакта еще нет
            if (_indexOldSelected != -1)
            {
                // снять выделение с предыдущего елемента
                _contactsVM[_indexOldSelected].IsSelected = false;
            }
           
            // выделить текущий
            _contactsVM[_currentSelectedIndex].IsSelected = true;

            // сохраняем индекс текущего выделенного елемента
            _indexOldSelected = _view.ListContacts.SelectedIndex;

            // имя контакта в заголовке дерева сообщений
            NameSelectedContact = _contactsVM[_currentSelectedIndex].Name;

            // очистить дерево сообщений от предыдущего контакта
            _view.TreeMessages.Items.Clear();

            // идентификатор выбранного пользователя
            _idSelectedUser = _contactsVM[_currentSelectedIndex].UserID;

            // новое сообщение прочитано
            _contactsVM[_currentSelectedIndex].NewContentAvailable = false;
            ContactsModel.Contacts![_currentSelectedIndex].NewContentAvailable = false;

            // создать модель дерева сообщений
            // передать id выделенного контакта и загрузить сообщения в модель
            TreeMessagesContactModel treeMessagesModel = new TreeMessagesContactModel(_idSelectedUser);

            foreach (Message message in treeMessagesModel.TreeMessagesContact!)
            {
                // создать модель представления сообщения
                MessageVM messageVM = new MessageVM();

                // проинициализировать VM из M
                messageVM.TextMessage = message.TextMessage;
                messageVM.MessageSentTime = message.MessageSentTime;
                messageVM.SentByMe = message.SentByMe;
                messageVM.LocalFilePath = message.LocalFilePath;
                // инициализация VM из VM контакта
                messageVM.Initials = _contactsVM[_currentSelectedIndex].Initials;
                messageVM.ProfilePictureRGB = _contactsVM[_currentSelectedIndex].ProfilePictureRGB;

                // добавить в коллекцию VM
                _messageVM.Add(messageVM);

                // создать пузырь сообщения
                MessageControl messageControl = new MessageControl();
                // связать V с VM
                messageControl.DataContext = messageVM;

                // добавить сообщение в ListBox
                AddToListBox(_view.TreeMessages, messageControl);
            }

            if (_view.TreeMessages.Items.Count >= 2)
            {
                // прокрутить ListBox в конец
                _view.TreeMessages.ScrollIntoView(_view.TreeMessages.Items[_view.TreeMessages.Items.Count - 2]);
            }
        }

        // загрузка контактов в список контактов
        private void LoadingContacts()
        {
            // связываем view-viewmodel-model
            foreach (Contact item in ContactsModel.Contacts)
            {
                // создаем модель-представления для каждого контакта
                CreateContactVM(item);
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

        // создать модель-представления контакта
        private void CreateContactVM(Contact contact)
        {
            // создать модель представления контакта
            ContactVM contactVM = new ContactVM();

            // связать VM c M
            contactVM.UserID = contact.UserID;
            contactVM.Name = contact.Name;
            contactVM.Message = contact.Message;
            contactVM.Initials = contact.Initials;
            contactVM.ProfilePictureRGB = contact.ProfilePictureRGB;
            contactVM.NewContentAvailable = contact.NewContentAvailable;

            // добавить в коллекцию
            _contactsVM.Add(contactVM);

            // создать представление контакта
            ContactControl contactControl = new ContactControl();
            // связать пердставление с модель представления 
            contactControl.DataContext = contactVM;

            // добавить представление контакта в список контактов в чате
            AddToListBox(_view.ListContacts, contactControl);
        }

        // отправка сообщения на сервер
        private async Task SendMessageToServerAsync()
        {
            OutgoingMessage message = new OutgoingMessage();
            // отправляемое сообщение
            message.Message = PendingMessageText;
            // id отправителя
            message.UserId = DetailsProfileModel.UserID;
            // id получателя. Находится в списке контактов
            message.RecipientId = _contactsVM[_currentSelectedIndex].UserID;
            // время отправки сообщения
            message.MessageSentTime = _messageSentTime;

            // сформировать данные для отправки на сервер
            DataPackage package = new DataPackage();
            // тип пакета
            package.Package = TypeData.Message;
            // основные данные пакета
            package.StringSerialize = JsonSerializer.Serialize(message);

            // отправить данные на сервер
            await WorkWithServer.SendMessageAsync(JsonSerializer.Serialize(package));
        }

        // сохранить сообщение в истории
        private void AddMessageToStory(string userID, MessageVM message)
        {
            using (FileStream fs = new FileStream(CreatePath(userID), FileMode.Open))
            {
                // если файл не пуст
                if (fs.Length != 0)
                {
                    // то дописываем новый контакт в коллекцию
                    fs.Seek(-1, SeekOrigin.End);
                    fs.Write(Encoding.UTF8.GetBytes(","));
                    JsonSerializer.Serialize(fs, message);
                    fs.Write(Encoding.UTF8.GetBytes("]"));
                }
                else
                {
                    // создаем коллекцию
                    fs.Write(Encoding.UTF8.GetBytes("["));
                    JsonSerializer.Serialize(fs, message);
                    fs.Write(Encoding.UTF8.GetBytes("]"));
                }
            }
        }

        // создать путь к истории переписки
        private string CreatePath(string userID)
        {
            // путь к истории переписки
            string path = Environment.CurrentDirectory;
            int index = path.LastIndexOf("WebChatClient");
            path = path.Remove(index + "WebChatClient".Length);
            path += $@"\db\UsersStories\{userID}.json";

            return path;
        }

        // вызывается по событию прихода нового сообщения
        private void WaitingIncomingMessage(string str)
        {
            try
            {
                // десериализация входящего сообщения
                IncomingMessage? incomingMessage = JsonSerializer.Deserialize<IncomingMessage>(str);

                if (incomingMessage != null)
                {
                    // создание модели-представления сообщения
                    MessageVM messageVM = new MessageVM()
                    {
                        TextMessage = incomingMessage.Message,
                        SentByMe = false,
                        MessageSentTime = incomingMessage.MessageSentTime,
                        LocalFilePath = string.Empty,
                        ProfilePictureRGB = _contactsVM[_currentSelectedIndex].ProfilePictureRGB
                    };

                    // сохраняем сообщение в историю
                    AddMessageToStory(incomingMessage.UserId, messageVM);

                    // найти контакт в моделе контактов который отправил пользователю сообщение
                    var findContact = ContactsModel.Contacts?.FirstOrDefault(o => o.UserID.CompareTo(incomingMessage.UserId) == 0);
                    // найти контакт в VM который отправил пользователю сообщение
                    var findContactVM = _contactsVM.FirstOrDefault(o => o.UserID.CompareTo(incomingMessage.UserId) == 0);

                    // истина, если контакт отправивший сообщение выделенный в списке контактов
                    if (_idSelectedUser.CompareTo(incomingMessage.UserId) == 0)
                    {
                        // инициалы контакта отправившего сообщение
                        messageVM.Initials = _contactsVM[_currentSelectedIndex].Initials;

                        // Добавить сообщение в список
                        _messageVM.Add(messageVM);

                        // создание и привязка VM
                        MessageControl messageControl = new MessageControl();
                        messageControl.DataContext = messageVM;

                        // добавить представление в ListBox
                        AddToListBox(_view.TreeMessages, messageControl);

                        // прокрутить ListBox в конец
                        _view.TreeMessages.ScrollIntoView(_view.TreeMessages.Items[_view.TreeMessages.Items.Count - 1]);
                    }
                    else 
                    {
                        if (findContact != null && findContactVM != null)
                        {
                            // если контакт отправивший сообщение, в данный момент не выделенный в списке контактов,
                            // то отмечаем это как новое сообщение от контакта
                            ContactsModel.Contacts![ContactsModel.Contacts!.IndexOf(findContact)].NewContentAvailable = true;
                            _contactsVM[_contactsVM.IndexOf(findContactVM)].NewContentAvailable = true;
                        }
                    }

                    if (findContact != null && findContactVM != null)
                    {
                        // последние сообщение контакта
                        ContactsModel.Contacts![ContactsModel.Contacts!.IndexOf(findContact)].Message = incomingMessage.Message;
                        _contactsVM[_contactsVM.IndexOf(findContactVM)].Message = incomingMessage.Message;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
