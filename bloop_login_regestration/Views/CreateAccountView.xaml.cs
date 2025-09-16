using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using bloop_login_regestration.Model;
using bloop_login_regestration.Services;

namespace bloop_login_regestration.Views
{
    
    public partial class CreateAccountView : UserControl
    {
        public CreateAccountView()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            await RegisterUserAsync();
        }

        private async Task RegisterUserAsync()
        {
            var client = new HttpClient();

            string plainPassword = new NetworkCredential(string.Empty, passwordBox.Password).Password;

            var newUser = new
            {
                fio = txtFullName.Text,
                email = txtGmail.Text,
                login = txtName.Text,
                phone = txtPhone.Text,
                password = plainPassword,
                isEmailConfirmed = false
            };

            var json = JsonSerializer.Serialize(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://localhost:7214/Auth/RegisterUser", content);
                response.EnsureSuccessStatusCode();

                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    UserSession.CurrentUser = user;

                    // Navigate to Welcome screen
                    var parent = Window.GetWindow(this) as MainWindow;
                    parent?.NavigateTo(new WelcomeBloopView());
                }
                else
                {
                    MessageBox.Show("Помилка: сервер не повернув користувача");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при реєстрації: " + ex.Message);
            }
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this) as MainWindow;
            parent?.NavigateTo(new LoginView());
        }
    }
}
