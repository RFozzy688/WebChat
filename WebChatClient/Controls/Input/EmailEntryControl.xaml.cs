﻿using System.Diagnostics;
using System.Windows;
using System;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для TextEntryControl.xaml
    /// </summary>
    public partial class EmailEntryControl : UserControl
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
                typeof(EmailEntryControl), 
                new PropertyMetadata(GridLength.Auto, LabelWidthChangedCallback));
        public EmailEntryControl()
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
                (d as EmailEntryControl).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }

#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                // Сообщить о потенциальной проблеме
                Debugger.Break();

                (d as EmailEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }
    }
}
