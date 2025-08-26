using System;
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
    /// Interaction logic for EmailVerificationView.xaml
    /// </summary>
    public partial class EmailVerificationView : UserControl
    {
        public EmailVerificationView()
        {
            InitializeComponent();
        }

        private void Resend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackLogin_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this) as MainWindow;
            parent?.NavigateTo(new LoginView());
        }

        private void ChangeEmail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
