using System.Diagnostics;
using System.Windows;
using System;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для TextEntryControl.xaml
    /// </summary>
    public partial class TextEntryControl : UserControl
    {
        // Ширина метки элемента управления
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        //Использование свойства зависимостей в качестве резервного хранилища для ширины метки.
        //Это обеспечивает анимацию, стилизацию, привязку и т.д.
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", 
                typeof(GridLength), 
                typeof(TextEntryControl), 
                new PropertyMetadata(GridLength.Auto, LabelWidthChangedCallback));
        public TextEntryControl()
        {
            InitializeComponent();

            //DataContext = new TextEntryVM();
        }

        /// <summary>
        /// Вызывается, когда ширина лейбла изменилась.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void LabelWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                // Установите ширину определения столбца на новое значение
                (d as TextEntryControl).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }

#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                // Сообщить о потенциальной проблеме
                Debugger.Break();

                (d as TextEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }
    }
}
