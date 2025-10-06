using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using bloop_login_regestration.Model;
using bloop_login_regestration.Services;

namespace bloop_login_regestration.Views
{
    /// <summary>
    /// Interaction logic for CreateAccountView.xaml
    /// </summary>
    public partial class CreateAccountView : UserControl
    {
        public CreateAccountView()
        {
            InitializeComponent();
        }

        // This is the click handler name you already use in XAML: Click="btnRegister_Click"
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            await RegisterUserAsync();
        }

        private async Task RegisterUserAsync()
        {
            try
            {
                using var client = new HttpClient();

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

                var response = await client.PostAsJsonAsync("https://localhost:7214/Auth/RegisterUser", newUser);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    if (user != null)
                    {
                        UserSession.CurrentUser = user;

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            var home = new HomeWindow(user);

                            // Load MainPageView right after registration
                            home.NavigateTo(new MainPageView());

                            home.Show();

                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = home;
                        });
                    }
                    else
                    {
                        MessageBox.Show("Помилка: сервер не повернув користувача");
                    }
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Registration failed: {response.StatusCode}\n{body}");
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
