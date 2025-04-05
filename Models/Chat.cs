using System.ComponentModel.DataAnnotations.Schema;
using chat_api.Models.Shared;

namespace chat_api.Models;

[Table("chat")]
public class Chat : BaseModel<int>
{
    [Column("title")] public string Title { get; set; }

    [Column("is_direct_chat")] public bool IsDirectChat { get; set; }
}