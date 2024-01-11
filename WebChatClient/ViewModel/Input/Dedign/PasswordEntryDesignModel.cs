
namespace WebChatClient
{
    public class PasswordEntryDesignModel : PasswordEntryVM
    {
        // Один экземпляр проектной модели
        public static PasswordEntryDesignModel Instance => new PasswordEntryDesignModel();

        public PasswordEntryDesignModel()
        {
            Label = "Name";
            FakePassword = "********";
        }
    }
}
