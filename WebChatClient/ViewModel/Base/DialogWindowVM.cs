using System.Windows;
using System.Windows.Controls;


namespace WebChatClient
{
    /// <summary>
    /// Модель представления для пользовательского окна
    /// </summary>
    public class DialogWindowVM : MainWindowVM
    {
        // Заголовок этого диалогового окна
        public string Title { get; set; }

        // Содержимое для размещения внутри диалогового окна
        public Control Content { get; set; }

        public DialogWindowVM(Window window) : base(window) 
        {
            // Уменьшить минимальный размер
            WindowMinimumWidth = 250;
            WindowMinimumHeight = 100;

            // Сделать строку заголовка меньше
            TitleHeight = 50;
        }
    }
}
