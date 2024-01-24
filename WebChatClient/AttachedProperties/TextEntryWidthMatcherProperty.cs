using System;
using System.Windows;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Согласуйте ширину меток всех элементов управления текста внутри этой панели.
    /// </summary>
    public class TextEntryWidthMatcherProperty : BaseAttachedProperty<TextEntryWidthMatcherProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Получите панель (обычно сетку)
            var panel = (sender as Panel);

            // Нначальная ширина набора
            SetWidths(panel);

            // Подождите, пока загрузится панель
            RoutedEventHandler onLoaded = null;
            onLoaded = (s, ee) =>
            {
                // Отцепить
                panel.Loaded -= onLoaded;

                // Установить ширину
                SetWidths(panel);

                // обход каждого ребенка
                foreach (var child in panel.Children)
                {
                    // Игнорируйте любые элементы управления с не текстовым вводом
                    if (!(child is TextEntryControl control))
                        continue;

                    control.Label.SizeChanged += (ss, eee) =>
                    {
                        // Обновить ширину
                        SetWidths(panel);
                    };
                }
            };

            // Подключитесь к событию Loaded
            panel.Loaded += onLoaded;
        }

        /// <summary>
        /// Обновите все дочерние элементы управления текста, чтобы их 
        /// ширина соответствовала наибольшей ширине группы
        /// </summary>
        /// <param name="panel">Панель с элементами управления текста</param>
        private void SetWidths(Panel panel)
        {
            // Следите за максимальной шириной
            var maxSize = 0d;

            foreach (var child in panel.Children)
            {
                // Игнорируйте любые элементы управления с не текстовым вводом.
                if (!(child is TextEntryControl control))
                    continue;

                // Найдите, больше ли это значение, чем другие элементы управления
                maxSize = Math.Max(maxSize, control.Label.RenderSize.Width + control.Label.Margin.Left + control.Label.Margin.Right);
            }

            // Создайте преобразователь длины сетки
            var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());

            foreach (var child in panel.Children)
            {
                // Игнорируйте любые элементы управления нетекстовым вводом.
                if (!(child is TextEntryControl control))
                    continue;

                // Установите для каждого элемента управления значение ширины метки на максимальный размер.
                control.LabelWidth = gridLength;
            }
        }
    }
}
