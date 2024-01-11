namespace WebChatClient
{
    /// <summary>
    /// Данные времени разработки для <see cref="MessageBoxDialogViewModel"/>
    /// </summary>
    public class MessageBoxDialogDesignModel : MessageBoxDialogVM
    {
        // Один экземпляр проектной модели
        public static MessageBoxDialogDesignModel Instance => new MessageBoxDialogDesignModel();

        public MessageBoxDialogDesignModel()
        {
            OkText = "OK";
            Message = "Design time messages are fun :)";
        }
    }
}
