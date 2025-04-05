using System.ComponentModel.DataAnnotations.Schema;

namespace chat_api.Models;

[Table("chat_person")]
public class ChatPerson
{
    [Column("id")] public int Id { get; set; }

    [Column("chat_id")] public int ChatId { get; set; }

    [Column("person_id")] public int PersonId { get; set; }
}