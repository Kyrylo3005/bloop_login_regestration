using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using bloop_login_regestration.Views;

namespace bloop_login_regestration
{
    public partial class MainWindow : Window
    {
        private bool isWindowMaximized = true;
        private Rect previousBounds;

        public MainWindow()
        {
            InitializeComponent();

            var loginView = new LoginView();
            MainContent.Content = loginView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Полный экран по умолчанию
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

        private void btnMinimase_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
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
        public void NavigateTo(UserControl nextView)
        {
            MainContent.Content = nextView;
        }

    }
}
