
namespace WebChatClient
{
    public class TextEntryDesignModel : TextEntryVM
    {
        // Один экземпляр проектной модели
        public static TextEntryDesignModel Instance => new TextEntryDesignModel();

        public TextEntryDesignModel()
        {
            Label = "Name";
            OriginalText = "Luke Malpass";
            EditedText = "Editing :)";
        }
    }
}
