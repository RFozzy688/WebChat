namespace WebChatClient
{ 
    /// <summary>
    /// Подробности диалогового окна сообщений
    /// </summary>
    public class MessageBoxDialogVM : BaseDialogVM
    {
        // Сообщение для отображения
        public string Message { get; set; }

        // Текст, используемый для кнопки ОК
        public string OkText { get; set; } = "OK";
    }
}
