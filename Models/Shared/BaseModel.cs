using System.ComponentModel.DataAnnotations.Schema;

namespace chat_api.Models.Shared;

public class BaseModel<T>
{
    public BaseModel()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    [Column("id")] public T Id { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("updated_at")] public DateTime UpdatedAt { get; set; }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}