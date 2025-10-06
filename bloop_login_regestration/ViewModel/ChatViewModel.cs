using bloop_login_regestration.Model;
using bloop_login_regestration.Services;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bloop_login_regestration.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;
        private readonly int _chatId;

        public ObservableCollection<MessageDto> Messages { get; set; } = new();
        public string NewMessage { get; set; }

        public ICommand SendMessageCommand { get; }
        public ICommand LoadMessagesCommand { get; }

        public ChatViewModel(int chatId)
        {
            _chatId = chatId;
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7214/") };

            SendMessageCommand = new RelayCommand(async _ => await SendMessage());
            LoadMessagesCommand = new RelayCommand(async _ => await LoadMessages());
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            var dto = new { ChatId = _chatId, SenderId = UserSession.CurrentUser.Id, Content = NewMessage };

            var response = await _httpClient.PostAsJsonAsync("Chat/SendMessage", dto);
            if (response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadFromJsonAsync<MessageDto>();
                if (msg != null) Messages.Add(msg);
                NewMessage = string.Empty;
                OnPropertyChanged(nameof(NewMessage));
            }
        }

        private async Task LoadMessages()
        {
            var msgs = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"Chat/GetMessages/{_chatId}");
            if (msgs != null)
            {
                Messages.Clear();
                foreach (var m in msgs) Messages.Add(m);
            }
        }
    }
}
