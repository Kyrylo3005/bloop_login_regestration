public class MessageViewModel
{
    public int Id { get; set; }
    public int ChatId { get; set; }           // optional if you use it
    public int SenderId { get; set; }         // server provides id
    public string SenderName { get; set; } = string.Empty; // optional display
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsOwnMessage { get; set; }    // true when the message belongs to current user
}
