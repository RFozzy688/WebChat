using System.Threading.Tasks;

namespace WebChatClient
{
    /// <summary>
    /// Менеджер пользовательского интерфейса, который обрабатывает любое 
    /// взаимодействие пользовательского интерфейса в приложении
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Отображает пользователю одно окно сообщения
        /// </summary>
        /// <param name="viewModel">Модель представления</param>
        /// <returns></returns>
        Task ShowMessage(MessageBoxDialogVM viewModel);
    }
}
