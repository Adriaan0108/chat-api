using System.ComponentModel.DataAnnotations.Schema;
using chat_api.Models.Shared;

namespace chat_api.Models;

[Table("message")]
public class Message : BaseModel<int>
{
    [Column("sender_id")] public int SenderId { get; set; }

    [Column("chat_id")] public int ChatId { get; set; }

    // [Column("recipient_id")] public int RecipientId { get; set; }

    [Column("text")] public string Text { get; set; }
}