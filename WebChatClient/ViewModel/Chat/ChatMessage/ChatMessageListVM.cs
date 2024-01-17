using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления списка цепочек сообщений в чата
    /// </summary>
    public class ChatMessageListVM : BaseViewModel
    {
        // Последний искомый текст в этом списке
        protected string _lastSearchText;

        // Текст для поиска в команде поиска
        protected string _searchText;

        // Элементы ветки чата для списка
        protected ObservableCollection<ChatMessageListItemVM> _items;

        // Флаг, указывающий, открыт ли диалог поиска
        protected bool _searchIsOpen;

        // Элементы ветки чата для списка
        public ObservableCollection<ChatMessageListItemVM> Items
        {
            get => _items;
            set
            {
                // Убедитесь, что список изменился
                if (_items == value)
                    return;

                // Обновить значение
                _items = value;

                // Обновить отфильтрованный список для соответствия
                FilteredItems = new ObservableCollection<ChatMessageListItemVM>(_items);
            }
        }

        // Элементы ветки чата для списка, включающие любые поисковые фильтры
        public ObservableCollection<ChatMessageListItemVM> FilteredItems { get; set; }

        // Название этого списка чатов
        public string DisplayTitle { get; set; }

        // True, чтобы показать меню вложений, false, чтобы скрыть его.
        public bool AttachmentMenuVisible { get; set; }

        // Истинно, если видны всплывающие меню.
        public bool AnyPopupVisible => AttachmentMenuVisible;

        // Текст для текущего сообщения
        public string PendingMessageText { get; set; }

        // Текст для поиска, когда мы выполняем поиск
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                // Обновить значение
                _searchText = value;

                // если текст поиска пуст...
                if (string.IsNullOrEmpty(SearchText))
                {
                    // Поиск для восстановления сообщений
                    Search();
                }
            }
        }

        // Флаг, указывающий, открыт ли диалог поиска
        public bool SearchIsOpen
        {
            get => _searchIsOpen;
            set
            {
                if (_searchIsOpen == value)
                    return;

                // Обновить значение
                _searchIsOpen = value;

                // Если диалог закрывается...
                if (!_searchIsOpen)
                {
                    // Очистить текст поиска
                    SearchText = string.Empty;
                }
            }
        }

        // Модель представления меню вложений
        public ChatAttachmentPopupMenuVM AttachmentMenu { get; set; }

        // Команда для нажатия кнопки вложения
        public ICommand AttachmentButtonCommand { get; set; }

        // Команда для щелчка по области за пределами всплывающего окна.
        public ICommand PopupClickawayCommand { get; set; }

        // Команда, когда пользователь нажимает кнопку отправки
        public ICommand SendCommand { get; set; }

        // Команда, когда пользователь хочет выполнить поиск
        public ICommand SearchCommand { get; set; }

        // Команда, когда пользователь хочет открыть диалоговое окно поиска
        public ICommand OpenSearchCommand { get; set; }

        // Команда, когда пользователь хочет закрыть диалоговое окно поиска
        public ICommand CloseSearchCommand { get; set; }

        // Команда, когда пользователь хочет очистить текст поиска
        public ICommand ClearSearchCommand { get; set; }

        public ChatMessageListVM()
        {
            // Создание команд
            AttachmentButtonCommand = new Command((o) => AttachmentButton());
            PopupClickawayCommand = new Command((o) => PopupClickaway());
            SendCommand = new Command((o) => Send());
            SearchCommand = new Command((o) => Search());
            OpenSearchCommand = new Command((o) => OpenSearch());
            CloseSearchCommand = new Command((o) => CloseSearch());
            ClearSearchCommand = new Command((o) => ClearSearch());

            // Сделать меню по умолчанию
            AttachmentMenu = new ChatAttachmentPopupMenuVM();
        }

        // Когда пользователь нажимает кнопку отправить, отправляет сообщение
        public void Send()
        {
            // Не отправляйте пустое сообщение
            if (string.IsNullOrEmpty(PendingMessageText))
                return;

            // Убедитесь, что списки не равны нулю
            if (Items == null)
            {
                Items = new ObservableCollection<ChatMessageListItemVM>();
            }

            if (FilteredItems == null)
            {
                FilteredItems = new ObservableCollection<ChatMessageListItemVM>();
            }

            // отправить фейковое новое сообщение
            var message = new ChatMessageListItemVM
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Luke Malpass",
                NewItem = true
            };

            // Добавить сообщение в оба списка
            Items.Add(message);
            FilteredItems.Add(message);

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

        // Выполняет поиск в текущем списке сообщений и фильтрует представление
        public void Search()
        {
            // Убедитесь, что мы не ищем повторно один и тот же текст
            if ((string.IsNullOrEmpty(_lastSearchText) && string.IsNullOrEmpty(SearchText)) ||
                string.Equals(_lastSearchText, SearchText))
                return;

            // Если у нас нет текста для поиска или элементов
            if (string.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                // Сделать отфильтрованный список одинаковым
                FilteredItems = new ObservableCollection<ChatMessageListItemVM>(Items);

                //Установить последний текст поиска
                _lastSearchText = SearchText;

                return;
            }

            // Найти все элементы, содержащие заданный текст
            // TODO: Сделайте поиск более эффективным
            FilteredItems = new ObservableCollection<ChatMessageListItemVM>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText)));

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
                SearchIsOpen = false;
            }
        }

        // Открывает диалог поиска
        public void OpenSearch() => SearchIsOpen = true;

        // Закрывает диалог поиска
        public void CloseSearch() => SearchIsOpen = false;
    }
}
