﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for ForgetPasswordView.xaml
    /// </summary>
    public partial class ForgetPasswordView : UserControl
    {
        public ForgetPasswordView()
        {
            InitializeComponent();
        }

        private void BackLogin_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this) as MainWindow;
            parent?.NavigateTo(new LoginView());

            //var parent = Window.GetWindow(this) as MainWindow;
            //parent?.NavigateTo(new ForgetPasswordView());
        }
    }

}
