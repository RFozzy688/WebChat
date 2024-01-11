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

        // Модель представления, которая будет использоваться для текущей страницы
        // при изменении текущей страницы
        // NOTE: Это не актуальная модель просмотра текущей страницы
        // он просто используется для установки модели представления текущей страницы
        // в момент изменения
        public BaseViewModel CurrentPageViewModel { get; set; }

        // True, если боковое меню должно отображаться
        public bool SideMenuVisible { get; set; } = true;

        // True, если меню настроек должно отображаться
        public bool SettingsMenuVisible { get; set; }

        /// <summary>
        /// Переход на указанную страницу
        /// </summary>
        /// <param name="page">Страница, на которую нужно перейти</param>
        public void GoToPage(AppPage page, BaseViewModel viewModel = null)
        {
            // Всегда скрывать страницу настроек, если мы меняем страницы
            SettingsMenuVisible = false;

            // Установите модель представления
            CurrentPageViewModel = viewModel;

            // Установить текущую страницу
            CurrentPage = page;

            // Запуск события изменения текущей страницы
            OnPropertyChanged(nameof(CurrentPage));

            // Показывать боковое меню или нет?
            SideMenuVisible = page == AppPage.Chat;
        }
    }
}
