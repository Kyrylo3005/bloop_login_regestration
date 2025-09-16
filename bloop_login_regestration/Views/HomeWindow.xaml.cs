using System;
using System.Windows;
using System.Windows.Input;
using bloop_login_regestration.Model;
using bloop_login_regestration.Services;

namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private bool isWindowMaximized = true;
        private Rect previousBounds;

        // Parameterless constructor (keeps compatibility)
        public HomeWindow()
        {
            InitializeComponent();
        }

        // Constructor used when logging in / passing the user
        public HomeWindow(User user) : this()
        {
            if (user != null)
            {
                // store user in session so other views can read it
                UserSession.CurrentUser = user;
            }
        }

        private void btnMinimase_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnToggleSize_Click(object sender, RoutedEventArgs e)
        {
            if (isWindowMaximized)
            {
                previousBounds = new Rect(Left, Top, Width, Height);

                Width = 875;
                Height = 550;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
                WindowState = WindowState.Normal;

                Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left;
                Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top;

                isWindowMaximized = false;
            }
            else
            {
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                Left = SystemParameters.WorkArea.Left;
                Top = SystemParameters.WorkArea.Top;

                isWindowMaximized = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Start maximized by default
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            Left = SystemParameters.WorkArea.Left;
            Top = SystemParameters.WorkArea.Top;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !isWindowMaximized)
                DragMove();
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            // Hook-up to open the main page (currently placeholder)
            // You can navigate MainContent here if needed
        }

        private void btnProfileCamera_Click(object sender, RoutedEventArgs e)
        {
            // Profile camera click handler (placeholder)
        }
    }
}
