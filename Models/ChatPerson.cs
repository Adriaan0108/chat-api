using System.ComponentModel.DataAnnotations.Schema;

namespace chat_api.Models;

public class ChatPerson
{
    public int Id { get; set; }

    [Column("chat_id")] public int ChatId { get; set; }

    [Column("person_id")] public int PersonId { get; set; }
}