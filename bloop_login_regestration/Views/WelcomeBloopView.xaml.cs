using System.Windows;
using System.Windows.Controls;
using bloop_login_regestration.Services;
using bloop_login_regestration.Model;

namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for WelcomeBloopView.xaml
    /// </summary>
    public partial class WelcomeBloopView : UserControl
    {
        public WelcomeBloopView()
        {
            InitializeComponent();
        }

        private void StartCommunication_Click(object sender, RoutedEventArgs e)
        {
            OpenHomeWindow();
        }



        private void OpenHomeWindow()
        {
            // Get the currently logged-in user from the session
            var user = UserSession.CurrentUser;

            // If session is empty for any reason, create a minimal fallback user
            if (user == null)
            {
                user = new User { Fio = "Гість", Email = string.Empty, Login = string.Empty, Phone = string.Empty};
            }

            var home = new HomeWindow();
            home.Show();

            // Close parent window (the window that hosts this UserControl, usually MainWindow)
            var parent = Window.GetWindow(this);
            parent?.Close();
        }
    }
}
