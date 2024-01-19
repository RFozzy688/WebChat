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

        int _indexOldSelected;

        // Открывает текущую ветку сообщений
        public ICommand OpenMessageCommand { get; set; }

        public ChatPageVM(ChatPage view)
        {
            _view = view;
            
            // инициализация
            _contactsVM = new ObservableCollection<ContactVM>();
            _contactsModel = new ContactsModel();

            // Создание команд
            OpenMessageCommand = new Command((o) => OpenMessage());

            // загрузка контактов
            LoadingContacts();
        }

        // открыть ветку с сообщениями
        private void OpenMessage()
        {
            _contactsVM[_indexOldSelected].IsSelected = false;
            _contactsVM[_view.ListContacts.SelectedIndex].IsSelected = true;

            _indexOldSelected = _view.ListContacts.SelectedIndex;
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
