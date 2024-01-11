using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        // Модель представления для этого окна
        private DialogWindowVM _viewModel;

        // Модель представления для этого окна
        public DialogWindowVM ViewModel
        {
            get => _viewModel;
            set
            {
                // Установить новое значение
                _viewModel = value;

                // Обновить контекст данных
                DataContext = _viewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}
