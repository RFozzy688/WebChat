using System.Windows.Input;

namespace WebChatClient
{
    public class DialogMessageBoxVM : BaseViewModel
    {
        // прадставление окна с сообщением
        DialogMessageBox _view;

        // Высота строки заголовка окна
        public int TitleHeight { get; set; } = 50;

        //Наименьшая ширина окна.
        public double WindowMinimumWidth { get; set; } = 370;

        //Наименьшая высота окна.
        public double WindowMinimumHeight { get; set; } = 180;

        // заголовок окна сообщений
        public string Title { get; set; } = "Message";

        // текст сообщения
        public string MessageText { get; set; } = "Новое сообщение в этом окне";

        // Команда закрытия окна
        public ICommand CloseCommand { get; set; }

        // команда подтверждать
        public ICommand ConfirmCommand { get; set; }

        public DialogMessageBoxVM(DialogMessageBox dialog)
        {
            _view = dialog;

            // Создание команд
            CloseCommand = new Command((o) => _view.Close());
        }
    }
}
