using System.Threading.Tasks;
using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Реализация приложений <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Отображает пользователю одно окно сообщения
        /// </summary>
        /// <param name="viewModel">Модель представления</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogVM viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);

            //var tcs = new TaskCompletionSource<bool>();
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    try
            //    {
            //        new DialogWindow().ShowDialog();
            //    }
            //    finally
            //    {
            //        tcs.TrySetResult(true);
            //    }

            //});
            //return tcs.Task;
        }
    }
}
