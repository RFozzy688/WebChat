using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatClient
{
    /// <summary>
    /// Стили анимации страниц для появления/исчезновения
    /// </summary>
    public enum PageAnimationStyles
    {
        // Нет анимации
        None = 0,

        // Страница двигается справа к центру
        MovesFromRightToCenter = 1,

        // Страница двигается от центра в лево
        MovesFromCenterToLeft = 2,
    }
}
