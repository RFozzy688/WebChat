using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Прикрепленное свойство IsBusy для всех элементов управления которые заняты.
    /// </summary>
    public class IsBusyProperty : BaseAttachedProperty<IsBusyProperty, bool>
    {
    }
}
