
namespace WebChatClient
{
    public class SettingsDesignModel : SettingsVM
    {
        // Один экземпляр проектной модели
        public static SettingsDesignModel Instance => new SettingsDesignModel();

        public SettingsDesignModel()
        {
            Name = new TextEntryVM { Label = "Name", OriginalText = "Luke Malpass" };
            Username = new TextEntryVM { Label = "Username", OriginalText = "luke" };
            Password = new PasswordEntryVM { Label = "Password", FakePassword = "********" };
            Email = new TextEntryVM { Label = "Email", OriginalText = "contact@angelsix.com" };
        }
    }
}
