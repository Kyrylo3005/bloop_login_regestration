using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bloop_login_regestration.Services;
using bloop_login_regestration.ViewModel;
public class HomeViewModel : ViewModelBase
{
    private string _welcomeText;
    public string WelcomeText
    {
        get => _welcomeText;
        set
        {
            _welcomeText = value;
            OnPropertyChanged(nameof(WelcomeText));
        }
    }

    public HomeViewModel()
    {
        if (UserSession.CurrentUser != null)
        {
            WelcomeText = $"Вітаємо, {UserSession.CurrentUser.Fio}!";
        }
    }
}
