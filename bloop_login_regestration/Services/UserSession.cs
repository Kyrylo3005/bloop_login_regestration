using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bloop_login_regestration.Model;

namespace bloop_login_regestration.Services
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}
