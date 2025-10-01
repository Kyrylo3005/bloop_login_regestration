using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace bloop_login_regestration.ViewModel
{
    public class ForgetPasswordViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand SendResetCommand { get; }

        public ForgetPasswordViewModel()
        {
            SendResetCommand = new RelayCommand(async _ => await SendResetAsync());
        }

        private async Task SendResetAsync()
        {
            try
            {
                using var client = new HttpClient();

                // Important: server expects "Email" with capital E
                var payload = new { Email = Email };

                var response = await client.PostAsJsonAsync(
                    "https://localhost:7214/Auth/ForgotPassword", payload);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("A new password has been sent to your email.");

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var parent = Application.Current.MainWindow as MainWindow;
                        parent?.NavigateTo(new Views.EmailSentView
                        {
                            DataContext = new EmailSentViewModel(Email)
                        });
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Email not found.");
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Error: " + response.StatusCode + "\n" + body);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }
        }
    }
}
