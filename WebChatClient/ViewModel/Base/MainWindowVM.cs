using Fasetto.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    public class MainWindowVM
    {
        // Окно, которым управляет эта модель представления
        private Window _view;

        /// <summary>
        /// Помощник по изменению размера окна, который поддерживает правильный размер окна в различных состояниях.
        /// </summary>
        private WindowResizer _windowResizer;

        public MainWindowVM(Window view)
        {
            _view = view;

            // Создание команд
            MinimizeCommand = new Command((o) => _view.WindowState = WindowState.Minimized);
            MaximizeCommand = new Command((o) => {
                _view.WindowState ^= WindowState.Maximized;
                _view.MaxHeight = SystemParameters.WorkArea.Height;
                _view.MaxWidth = SystemParameters.WorkArea.Width;
            });
            CloseCommand = new Command((o) => _view.Close());
            MenuCommand = new Command((o) => SystemCommands.ShowSystemMenu(_view, GetMousePosition()));

            // Fix window resize issue
            _windowResizer = new WindowResizer(_view);
        }

        // Высота строки заголовка окна
        public int TitleHeight { get; set; } = 50;
        // Высота строки заголовка окна
        
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight);
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
        //Получает текущую позицию мыши на экране
        private Point GetMousePosition()
        {
            return _windowResizer.GetCursorPosition();
        }
    }
}
