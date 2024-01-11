using System.Diagnostics;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Преобразует <see cref="AppPage"/> в фактическое представление/страницу.
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Принимает <see cref="AppPage"/> и модель представления, если таковая имеется, и создает нужную страницу.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this AppPage page, object viewModel = null)
        {
            // Найдите подходящую страницу
            switch (page)
            {
                case AppPage.Login:
                    return new LoginPage(viewModel as LoginPageVM);

                case AppPage.Register:
                    return new RegisterPage(viewModel as RegisterPageVM);

                case AppPage.Chat:
                    return new ChatPage(viewModel as ChatMessageListVM);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Преобразует <see cref="BasePage"/> в конкретную <see cref="AppPage"/>, предназначенную для этого типа страницы
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static AppPage ToApplicationPage(this BasePage page)
        {
            // Найдите страницу приложения, соответствующую базовой странице.
            if (page is ChatPage)
                return AppPage.Chat;

            if (page is LoginPage)
                return AppPage.Login;

            if (page is RegisterPage)
                return AppPage.Register;

            Debugger.Break();
                return default(AppPage);
        }
    }
}
