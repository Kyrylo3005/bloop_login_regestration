using bloop_login_regestration.Model;
using bloop_login_regestration.ViewModel;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

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
            DataContext = new MainPageViewModel();
        }

        private async void Friend_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.Tag is not string friendIdStr || !int.TryParse(friendIdStr, out var friendId))
                return;

            var myId = Services.UserSession.CurrentUser.Id;

            using var http = new HttpClient { BaseAddress = new Uri("https://localhost:7214/") };
            var request = new { User1Id = myId, User2Id = friendId };
            var response = await http.PostAsJsonAsync("Chat/CreatePrivateChat", request);

            if (response.IsSuccessStatusCode)
            {
                var chatId = await response.Content.ReadFromJsonAsync<int>();
                var chatView = new ChatView(chatId);
                var home = (HomeWindow)Application.Current.MainWindow;
                home.NavigateTo(chatView);
            }
        }


        // Old placeholder forwarded click — kept for XAML references
        private void BubbleByte_Friend_Click(object sender, RoutedEventArgs e)
        {
            Friend_Click(sender, e);
        }

        private void OpenChat(int chatId)
        {
            var chatView = new ChatView(chatId);
            var home = (HomeWindow)Application.Current.MainWindow;
            home?.NavigateTo(chatView);
        }

        

    }
}
