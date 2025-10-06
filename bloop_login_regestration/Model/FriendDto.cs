namespace bloop_login_regestration.Model
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string Fio { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
    }
}
