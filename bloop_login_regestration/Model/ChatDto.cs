public class ChatDto
{
    public int Id { get; set; }
    public string Name { get; set; } // user display name
    public int OtherUserId { get; set; } // the friend’s ID
}