using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bloop_login_regestration.ViewModel
{
    public class EmailSentViewModel : ViewModelBase
    {
        private readonly string _email;
        public string Email => _email;

        public ICommand SendAgainCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public EmailSentViewModel(string email)
        {
            _email = email;

            SendAgainCommand = new RelayCommand(async _ => await SendAgainAsync());
            BackToLoginCommand = new RelayCommand(_ => NavigateBackToLogin());
        }

        private async Task SendAgainAsync()
        {
            try
            {
                using var client = new HttpClient();

                var payload = new { Email = _email };

                var response = await client.PostAsJsonAsync("https://localhost:7214/Auth/ForgotPassword", payload);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Password reset email has been sent again.");
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

        private void NavigateBackToLogin()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var parent = Application.Current.MainWindow as MainWindow;
                parent?.NavigateTo(new Views.LoginView());
            });
        }
    }
}
