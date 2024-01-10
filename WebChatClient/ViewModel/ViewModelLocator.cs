using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatCore;

namespace WebChatClient
{
    // Находит модели представления из IoC для использования при привязке в файлах Xaml.
    public class ViewModelLocator
    {
        // Одиночный экземпляр локатора
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        // Модель представления приложения
        public static AppVM AppVM => IoC.Get<AppVM>();

        // Модель просмотра настроек
        public static SettingsVM SettingsVM => IoC.Settings;
    }
}
