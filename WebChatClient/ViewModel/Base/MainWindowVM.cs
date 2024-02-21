using WindowHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Text.Json;

namespace WebChatClient
{
    public class MainWindowVM : BaseViewModel
    {
        // Окно, которым управляет эта модель представления
        private Window _view;

        // Помощник по изменению размера окна, который поддерживает правильный размер окна в различных состояниях.
        private WindowResizer _windowResizer;

        public MainWindowVM(Window view)
        {
            _view = view;

            var activeScreen = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position);
            _view.MaxHeight = activeScreen.WorkingArea.Height;
            _view.MaxWidth = activeScreen.WorkingArea.Width;

            // Создание команд
            MinimizeCommand = new Command((o) => _view.WindowState = WindowState.Minimized);
            MaximizeCommand = new Command((o) => _view.WindowState ^= WindowState.Maximized);
            CloseCommand = new Command((o) => 
            {
                // сохранить изменения в списке контактов при выходе
                ContactsModel.SaveAllContacts();

                // сформировать данные для отправки на сервер
                DataPackage package = new DataPackage();
                // тип пакета
                package.Package = TypeData.Exit;
                // основные данные пакета
                package.StringSerialize = DetailsProfileModel.UserID;

                // отправить данные на сервер
                WorkWithServer.SendMessage(JsonSerializer.Serialize(package));

                _view.Close();
            });
            MenuCommand = new Command((o) => SystemCommands.ShowSystemMenu(_view, GetMousePosition()));
            WorkingAreaCommand = new Command((o) => 
            {
                // размеры активного экрана, то есть там где находится окно приложения
                var activeScreen = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position);
                _view.MaxHeight = activeScreen.WorkingArea.Height;
                _view.MaxWidth = activeScreen.WorkingArea.Width;
            });
            ListenToPortCommand = new Command(async (o) =>
            {
                // создать сокеты
                WorkWithServer.ConnectTo();
                // слушать входящий порт
                await WorkWithServer.ReceiveMessageAsync();
            });

            // автоматически запустить команду при старте приложения
            ListenToPortCommand.Execute(null);

            // Fix window resize issue
            _windowResizer = new WindowResizer(_view);
        }
        // Определение рабочей области активного экрана
        public ICommand WorkingAreaCommand { get; set; }

        // Высота строки заголовка окна
        public int TitleHeight { get; set; } = 50;

        //Наименьшая ширина окна.
        public double WindowMinimumWidth { get; set; } = 800;

        //Наименьшая высота окна.
        public double WindowMinimumHeight { get; set; } = 500;

        // Команда сворачивания окна
        public ICommand MinimizeCommand { get; set; }

        // Команда максимизации окна
        public ICommand MaximizeCommand { get; set; }

        // Команда закрытия окна
        public ICommand CloseCommand { get; set; }

        // Команда показа системного меню окна
        public ICommand MenuCommand { get; set; }

        // Команда для прослушивания входящего порта
        public ICommand ListenToPortCommand { get; set; }

        // Текущая страница приложения
        public Page CurrentPage { get; set; } = new LoginPage();

        //Получает текущую позицию мыши на экране
        private Point GetMousePosition()
        {
            return _windowResizer.GetCursorPosition();
        }
    }
}
