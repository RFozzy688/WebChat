using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatCore;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления для любых всплывающих меню.
    /// </summary>
    public class BasePopupVM : BaseViewModel
    {
        // Цвет фона пузырька в значении ARGB.
        public string BubbleBackground { get; set; }

        // Выравнивание угла пузырька
        public ElementHorizontalAlignment CornerAlignment { get; set; }

        // Содержимое этого всплывающего меню
        public BaseViewModel Content { get; set; }

        public BasePopupVM()
        {
            // Установить значения по умолчанию
            BubbleBackground = "fafafa";
            CornerAlignment = ElementHorizontalAlignment.Left;
        }
    }
}
