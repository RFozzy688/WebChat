using System.Security;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления ввода пароля для редактирования пароля
    /// <summary>
    public class PasswordEntryVM : BaseViewModel
    {
        // Метка, определяющая, для чего предназначено это значение
        public string Label { get; set; }

        // Строка отображения поддельного пароля
        public string FakePassword { get; set; }

        // Текст подсказки текущего пароля
        public string CurrentPasswordHintText { get; set; }

        // Текст подсказки для нового пароля
        public string NewPasswordHintText { get; set; }

        // Текст подсказки для подтверждения пароля
        public string ConfirmPasswordHintText { get; set; }

        // Текущий сохраненный пароль
        public SecureString CurrentPassword { get; set; }

        // Текущий отредактированный пароль без фиксации
        public SecureString NewPassword { get; set; }

        // Текущий нефиксированный отредактированный подтвержденный пароль
        public SecureString ConfirmPassword { get; set; }

        // Указывает, находится ли текущий текст в режиме редактирования
        public bool Editing { get; set; }

        // Переводит элемент управления в режим редактирования
        public ICommand EditCommand { get; set; }

        // Выход из режима редактирования
        public ICommand CancelCommand { get; set; }

        // Фиксирует изменения и сохраняет значение, а также возвращается в режим без редактирования
        public ICommand SaveCommand { get; set; }

        public PasswordEntryVM()
        {
            // Создание команд
            EditCommand = new Command((o) => Edit());
            CancelCommand = new Command((o) => Cancel());
            SaveCommand = new Command((o) => Save());

            // Установить подсказки по умолчанию
            CurrentPasswordHintText = "Current Password";
            NewPasswordHintText = "New Password";
            ConfirmPasswordHintText = "Confirm Password";
        }

        // Переводит элемент управления в режим редактирования
        public void Edit()
        {
            // Очистить весь пароль
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            // Перейти в режим редактирования
            Editing = true;
        }

        // Отменяет выход из режима редактирования
        public void Cancel()
        {
            Editing = false;
        }

        // Фиксирует содержимое и выходит из режима редактирования
        public void Save()
        {
            // Убедитесь, что текущий пароль верен
            // TODO: Это будет получено из реального внутреннего хранилища паролей этого пользователя
            // или запросив подтверждение у веб-сервера
            var storedPassword = "test";

            // Подтвердите, что текущий пароль совпадает
            // NOTE: Обычно это делается не здесь, это делается на сервере.
            if (storedPassword != CurrentPassword.Unsecure())
            {
                // Сообщить пользователю
                //IoC.UI.ShowMessage(new MessageBoxDialogVM
                //{
                //    Title = "Wrong password",
                //    Message = "The current password is invalid"
                //});

                return;
            }

            // Теперь проверьте, что новый и подтверждающий пароль совпадают
            if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
            {
                // Сообщить пользователю
                //IoC.UI.ShowMessage(new MessageBoxDialogVM
                //{
                //    Title = "Password mismatch",
                //    Message = "The new and confirm password do not match"
                //});

                return;
            }

            // Проверьте, действительно ли у нас есть пароль
            if (NewPassword.Unsecure().Length == 0)
            {
                // Сообщить пользователю
                //IoC.UI.ShowMessage(new MessageBoxDialogVM
                //{
                //    Title = "Password too short",
                //    Message = "You must enter a password!"
                //});

                return;
            }

            // Установите для отредактированного пароля текущее значение
            CurrentPassword = new SecureString();

            foreach (var c in NewPassword.Unsecure().ToCharArray())
            {
                CurrentPassword.AppendChar(c);
            }

            Editing = false;
        }
    }
}
