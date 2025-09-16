using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

                var json = JsonSerializer.Serialize(new { email = Email });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7214/Auth/ForgotPassword", content);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("A new password has been sent to your email.");

                    // Navigate to EmailSentView
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var parent = Window.GetWindow(Application.Current.MainWindow.Content as FrameworkElement) as MainWindow;
                        parent?.NavigateTo(new Views.EmailSentView());
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Email not found.");
                }
                else
                {
                    MessageBox.Show("Error: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }
        }
    }
}
