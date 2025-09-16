using System.Windows;
using System.Windows.Controls;

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
            DataContext = new ViewModel.ForgetPasswordViewModel();
        }


        private void BackLogin_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this) as MainWindow;
            parent?.NavigateTo(new LoginView());
        }
    }
}
