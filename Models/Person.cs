using System.ComponentModel.DataAnnotations.Schema;
using chat_api.Models.Shared;

namespace chat_api.Models;

[Table("person")]
public class Person : BaseModel<int>
{
    [Column("username")] public string Username { get; set; }

    [Column("first_name")] public string FirstName { get; set; }

    [Column("last_name")] public string LastName { get; set; }

    [Column("secret")] public string Secret { get; set; }

    [Column("is_online")] public bool IsOnline { get; set; }
}