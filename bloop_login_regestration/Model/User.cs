using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model/User.cs
namespace bloop_login_regestration.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
