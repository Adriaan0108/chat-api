namespace chat_api.Dtos;

public class CreatePersonDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Secret { get; set; }
}