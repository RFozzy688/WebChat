using System.Security;

namespace WebChatClient
{
    /// <summary>
    /// Интерфейс для класса, который может предоставить безопасный пароль.
    /// </summary>
    public interface IHavePassword
    {
        /// <summary>
        /// Надежный пароль
        /// </summary>
        SecureString SecurePassword { get; }
    }
}
