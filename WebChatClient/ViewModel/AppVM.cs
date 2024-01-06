﻿using System;
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
        public AppPage CurrentPage { get; set; } = AppPage.Login;

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;
    }
}
