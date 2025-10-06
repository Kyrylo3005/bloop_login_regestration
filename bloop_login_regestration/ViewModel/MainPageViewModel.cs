using bloop_login_regestration.Model;
using bloop_login_regestration.Services;
using bloop_login_regestration.Views;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bloop_login_regestration.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<User> Users { get; set; } = new();

        public ICommand LoadUsersCommand { get; }
        public ICommand OpenChatCommand { get; }

        public MainPageViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7214/") };
            LoadUsersCommand = new RelayCommand(async _ => await LoadUsers());
            OpenChatCommand = new RelayCommand(OpenChat);

            _ = LoadUsers();
        }

        private void OpenChat(object parameter)
        {
            if (parameter is User user)
            {
                var chatView = new bloop_login_regestration.Views.ChatView(user.Id);
                var home = (HomeWindow)System.Windows.Application.Current.MainWindow;
                home.NavigateTo(chatView);
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                // just get all users (server must expose this GET /User endpoint)
                var users = await _httpClient.GetFromJsonAsync<List<User>>("User/All");
                if (users != null)
                {
                    Users.Clear();
                    foreach (var u in users)
                    {
                        if (u.Id != UserSession.CurrentUser.Id) // skip self
                            Users.Add(u);
                    }
                }
            }
            catch { /* handle errors */ }
        }
    }
}
