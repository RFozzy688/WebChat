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
        public AppPage CurrentPage { get; private set; } = AppPage.Login;

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// Переход на указанную страницу
        /// </summary>
        /// <param name="page">Страница, на которую нужно перейти</param>
        public void GoToPage(AppPage page)
        {
            // Установить текущую страницу
            CurrentPage = page;

            // Показывать боковое меню или нет?
            SideMenuVisible = page == AppPage.Chat;
        }
    }
}
