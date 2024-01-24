using System.Windows;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для DialogMessageBox.xaml
    /// </summary>
    public partial class DialogMessageBox : Window
    {
        public DialogMessageBox()
        {
            InitializeComponent();

            DataContext = new DialogMessageBoxVM(this);
        }
    }
}
