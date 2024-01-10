using System.Windows.Controls;


namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            DataContext = IoC.Settings;
        }
    }
}
