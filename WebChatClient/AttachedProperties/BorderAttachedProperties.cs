using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WebChatClient
{
    /// <summary>
    /// Создает область отсечения из родительского элемента <see cref="Border"/> <see cref="CornerRadius"/>
    /// </summary>
    public class ClipFromBorderProperty : BaseAttachedProperty<ClipFromBorderProperty, bool>
    {
        // Вызывается при первой загрузке родительской границы
        private RoutedEventHandler _border_Loaded;

        // Вызывается при изменении размера границы
        private SizeChangedEventHandler _border_SizeChanged;

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Получить себя
            var self = (sender as FrameworkElement);

            // Проверьте, есть ли у нас родительская граница
            if (!(self.Parent is Border border))
            {
                Debugger.Break();
                return;
            }

            // Настройка загруженного события
            _border_Loaded = (s1, e1) => Border_OnChange(s1, e1, self);

            // Событие изменения размера установки
            _border_SizeChanged = (s1, e1) => Border_OnChange(s1, e1, self);

            // Если это правда, подключиться к событиям
            if ((bool)e.NewValue)
            {
                border.Loaded += _border_Loaded;
                border.SizeChanged += _border_SizeChanged;
            }
            else
            {
                border.Loaded -= _border_Loaded;
                border.SizeChanged -= _border_SizeChanged;
            }
        }

        /// <summary>
        /// Вызывается, когда граница загружена и изменен размер.
        /// </summary>
        /// <param name="sender">Граница</param>
        /// <param name="e"></param>
        /// <param name="child">Дочерний элемент (мы сами)</param>
        private void Border_OnChange(object sender, RoutedEventArgs e, FrameworkElement child)
        {
            //Получить границу
            var border = (Border)sender;

            // Проверьте, есть ли у нас реальный размер
            if (border.ActualWidth == 0 && border.ActualHeight == 0)
                return;

            // Настройте новую дочернюю область обрезки
            var rect = new RectangleGeometry();

            // Сопоставьте угловой радиус с угловым радиусом границ
            rect.RadiusX = rect.RadiusY = Math.Max(0, border.CornerRadius.TopLeft - (border.BorderThickness.Left * 0.5));

            // Установите размер прямоугольника, соответствующий фактическому размеру ребенка
            rect.Rect = new Rect(child.RenderSize);

            // Назначьте область обрезки дочернему элементу
            child.Clip = rect;
        }
    }
}
