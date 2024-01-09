namespace WebChatCore
{
    /// <summary>
    /// Вспомогательные функции для <see cref="IconType"/>
    /// </summary>
    public static class IconTypeExtensions
    {
        /// <summary>
        /// Преобразует <see cref="IconType"/> в строку Font Awesome.
        /// </summary>
        /// <param name="type">Тип для преобразования</param>
        /// <returns></returns>
        public static string ToFontAwesome(this IconType type)
        {
            // Возвращает строку FontAwesome на основе типа значка.
            switch (type)
            {
                case IconType.File:
                    return "\uf0f6";

                case IconType.Picture:
                    return "\uf1c5";

                default:
                    return null;
            }
        }
    }
}
