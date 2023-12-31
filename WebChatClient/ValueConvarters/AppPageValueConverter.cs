﻿using System;
using System.Diagnostics;
using System.Globalization;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Преобразует ApplPage в фактическое представление/страницу.
    /// </summary>
    public class AppPageValueConverter : BaseValueConverter<AppPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AppPage)value) 
            {
                // поиск соответствующей стрвницы
                case AppPage.Register:
                    return new RegisterPage();
                case AppPage.Login:
                    return new LoginPage();
                case AppPage.Chat:
                    return new ChatPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
