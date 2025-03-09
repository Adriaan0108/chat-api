namespace chat_api.Dtos;

public class CreateChatDto
{
    public List<string> Usernames { get; set; }

    public string Title { get; set; }

    public bool IsDirectChat { get; set; }
}