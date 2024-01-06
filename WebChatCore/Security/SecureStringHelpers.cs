using System.Security;
using System.Runtime.InteropServices;

namespace WebChatCore
{
    /// <summary>
    /// Помощники для класса <see cref="SecureString"/>
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Снимает защиту <see cref="SecureString"/> защищённой строки.
        /// </summary>
        /// <param name="secureString">защищённая строка</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;

            // Получить указатель на незащищенную строку в памяти
            var unmanagedString = IntPtr.Zero;

            try
            {
                // Снимает пароль
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Очистить память
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
