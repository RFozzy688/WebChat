using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Пользовательский запуск, поэтому мы загружаем наш IoC сразу перед чем-либо еще.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            //базовое приложение делат то, что ему нужно.
            base.OnStartup(e);

            IoC.Setup();

            // Показать главное окно
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
