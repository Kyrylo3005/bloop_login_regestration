using bloop_login_regestration.Model;
using bloop_login_regestration.Services;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;

namespace bloop_login_regestration.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;
        private int _chatId;

        public ObservableCollection<MessageDto> Messages { get; set; } = new();
        public string NewMessage { get; set; } = string.Empty;

        public ICommand SendMessageCommand { get; }
        public ICommand LoadMessagesCommand { get; }

        public ChatViewModel(int chatId)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7214/") };
            _chatId = chatId;

            SendMessageCommand = new RelayCommand(async _ => await SendMessage());
            LoadMessagesCommand = new RelayCommand(async _ => await LoadMessages());
        }

        private async Task LoadMessages()
        {
            var result = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"Chat/GetMessages/{_chatId}");
            if (result != null)
            {
                Messages.Clear();
                foreach (var msg in result)
                    Messages.Add(msg);
            }
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage))
                return;

            var request = new
            {
                ChatId = _chatId,
                SenderId = UserSession.CurrentUser.Id,
                Content = NewMessage
            };

            var response = await _httpClient.PostAsJsonAsync("Chat/SendMessage", request);
            if (response.IsSuccessStatusCode)
            {
                var sentMessage = await response.Content.ReadFromJsonAsync<MessageDto>();
                if (sentMessage != null)
                {
                    Messages.Add(sentMessage);
                    NewMessage = string.Empty;
                    OnPropertyChanged(nameof(NewMessage));
                }
            }
        }
    }
}
