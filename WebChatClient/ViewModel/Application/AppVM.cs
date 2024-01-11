using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Состояние приложения как модель представления
    /// </summary>
    public class AppVM : BaseViewModel
    {
        // Текущая страница приложения
        public AppPage CurrentPage { get; private set; } = AppPage.Chat;

        // True, если боковое меню должно отображаться
        public bool SideMenuVisible { get; set; } = true;

        // True, если меню настроек должно отображаться
        public bool SettingsMenuVisible { get; set; }

        /// <summary>
        /// Переход на указанную страницу
        /// </summary>
        /// <param name="page">Страница, на которую нужно перейти</param>
        public void GoToPage(AppPage page)
        {
            // Всегда скрывать страницу настроек, если мы меняем страницы
            SettingsMenuVisible = false;

            // Установить текущую страницу
            CurrentPage = page;

            // Показывать боковое меню или нет?
            SideMenuVisible = page == AppPage.Chat;
        }
    }
}
