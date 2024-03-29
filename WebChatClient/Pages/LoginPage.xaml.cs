﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IHavePassword
    {
        public LoginPage()
        {
            InitializeComponent();

            DataContext = new LoginPageVM();
        }

        // Надежный пароль для этой страницы входа.
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
