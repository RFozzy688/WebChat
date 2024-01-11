using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Базовый класс для любого контента, который используется внутри <see cref="DialogWindow"/>
    /// </summary>
    public class BaseDialogUserControl : UserControl
    { 
        // Диалоговое окно, в котором мы будем находиться
        private DialogWindow _dialogWindow;

        // Закрывает это диалоговое окно
        public ICommand CloseCommand { get; private set; }

        // Минимальная ширина этого диалогового окна
        public int WindowMinimumWidth { get; set; } = 250;

        // Минимальная высота этого диалогового окна
        public int WindowMinimumHeight { get; set; } = 100;

        // Высота заголовка
        public int TitleHeight { get; set; } = 50;

        // Название этого диалога
        public string Title { get; set; }

        public BaseDialogUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                // Создать новое диалоговое окно
                _dialogWindow = new DialogWindow();
                _dialogWindow.ViewModel = new DialogWindowVM(_dialogWindow);

                // Создать команду закрытия
                CloseCommand = new Command((o) => _dialogWindow.Close());
            }
        }

        /// <summary>
        /// Отображает пользователю одно окно сообщения
        /// </summary>
        /// <param name="viewModel">Модель представления</param>
        /// <typeparam name="T">Тип модели представления для этого элемента управления</typeparam>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel)
            where T : BaseDialogVM
        {
            // Создайте задачу для ожидания закрытия диалога
            var tcs = new TaskCompletionSource<bool>();

            // Запуск в потоке пользовательского интерфейса
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Сопоставьте ожидаемые размеры элементов управления с моделью
                    // представления диалоговых окон.
                    _dialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    _dialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    _dialogWindow.ViewModel.TitleHeight = TitleHeight;
                    _dialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    // Установите этот элемент управления на содержимое диалогового окна.
                    _dialogWindow.ViewModel.Content = this;

                    // Настройка этого элемента управления привязкой контекста данных
                    // к модели представления.
                    DataContext = viewModel;

                    // Показать в центре родителя
                    _dialogWindow.Owner = Application.Current.MainWindow;
                    _dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    // Показать диалог
                    _dialogWindow.ShowDialog();
                }
                finally
                {
                    // Сообщите звонящему, что мы закончили
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}
