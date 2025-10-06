using bloop_login_regestration.Services;
using System;

namespace bloop_login_regestration.Model
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        
        public bool IsOwnMessage
        {
            get
            {
                return UserSession.CurrentUser != null &&
                       SenderId == UserSession.CurrentUser.Id;
            }
        }
    }
}
