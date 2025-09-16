using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bloop_login_regestration.Model;
using bloop_login_regestration.Services;
using bloop_login_regestration.Views;

namespace bloop_login_regestration.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(_ => NavigateToForgotPassword());
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return !string.IsNullOrEmpty(Username) && Password != null;
        }

        private void ExecuteLoginCommand(object obj)
        {
            _ = ExecuteLoginAsync();
        }

        private async Task ExecuteLoginAsync()
        {
            var httpClient = new HttpClient();
            try
            {
                var credentials = new
                {
                    email = Username,
                    password = new System.Net.NetworkCredential(string.Empty, Password).Password
                };

                var response = await httpClient.PostAsJsonAsync("https://localhost:7214/Auth/GetUserByEmail", credentials);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();

                    if (user != null)
                    {
                        UserSession.CurrentUser = user;

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            var home = new HomeWindow(user);
                            home.Show();

                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = home;
                        });
                    }
                    else
                    {
                        ErrorMessage = "User not found.";
                    }
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Connection error: " + ex.Message;
            }
        }

        private void NavigateToForgotPassword()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var parent = Application.Current.MainWindow as MainWindow;
                parent?.NavigateTo(new ForgetPasswordView());
            });
        }
    }
}
