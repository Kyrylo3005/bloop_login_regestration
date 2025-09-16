using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Text.Json;
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
        private bool _isViewVisible=true;

        public string Username { 
            get => _username; 
            set {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password { 
            get => _password; 
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage; set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible { 
            get => _isViewVisible; set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //constructor
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrEmpty(Username) || Username.Length < 4 || Password==null || Password.Length < 5)
                validData = false;
            else validData = true;
            return validData;
        }



        private void ExecuteLoginCommand(object obj)
        {
            _ = ExecuteLoginAsync(); // Запуск асинхронно
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

                var content = new StringContent(
                    JsonSerializer.Serialize(credentials),
                    Encoding.UTF8,
                    "application/json");

                var response = await httpClient.PostAsync("https://localhost:7214/Auth/GetUserByEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();

                    if (user != null)
                    {
                        UserSession.CurrentUser = user;

                        // Перехід до нового вікна
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
                        ErrorMessage = "Користувача не знайдено.";
                    }
                }
                else
                {
                    ErrorMessage = "Невірний email або пароль.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Помилка при з’єднанні: " + ex.Message;
            }
        }

    }
}
