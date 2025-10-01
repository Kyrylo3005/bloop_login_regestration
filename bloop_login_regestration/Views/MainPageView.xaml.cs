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
using System.Windows.Shapes;

namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for MainPageView.xaml
    /// </summary>
    public partial class MainPageView : UserControl
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        private void Friend_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)?.Tag?.ToString();

            if (string.IsNullOrEmpty(tag))
                return;

            int chatId = 0;

            // For now: map tags manually (later you’ll replace with real data from API)
            switch (tag)
            {
                case "BubbleByte":
                    chatId = 1;
                    break;
                case "Bloop":
                    chatId = 2;
                    break;
                default:
                    // Groups or others
                    chatId = 99;
                    break;
            }

            if (chatId > 0)
            {
                OpenChat(chatId);
            }
        }


        private void BubbleByte_Friend_Click(object sender, RoutedEventArgs e)
        {
            Friend_Click(sender, e);
        }
        private void OpenChat(int chatId)
        {
            var chatView = new ChatView(chatId);
            var home = (HomeWindow)Application.Current.MainWindow;
            home.NavigateTo(chatView);
        }

    }
}
