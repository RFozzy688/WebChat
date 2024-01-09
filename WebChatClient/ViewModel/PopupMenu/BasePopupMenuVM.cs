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
    public class BasePopupMenuVM : BaseViewModel
    {
        // Цвет фона пузырька в значении ARGB.
        public string BubbleBackground { get; set; }

        // Выравнивание угла пузырька
        public ElementHorizontalAlignment CornerAlignment { get; set; }

        public BasePopupMenuVM()
        {
            // Установить значения по умолчанию
            BubbleBackground = "fafafa";
            CornerAlignment = ElementHorizontalAlignment.Left;
        }
    }
}
