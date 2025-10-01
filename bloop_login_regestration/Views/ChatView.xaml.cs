using bloop_login_regestration.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {


        private readonly int _chatId;

        public ChatView(int chatId)
        {
            InitializeComponent();
            _chatId = chatId;
            DataContext = new ChatViewModel(_chatId);
        }



        private void Friend_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)?.Tag?.ToString();
            // TODO: open chat by tag / id
        }

        private void BubbleByte_Friend_Click(object sender, RoutedEventArgs e)
        {
            Friend_Click(sender, e);
        }

        private void OpenFullProfile_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this) as MainWindow;
            if (parent != null)
            {
                /*parent.NavigateTo(new ProfileView());*/ // replace with your actual ProfileView class
            }
        }
    }
}
