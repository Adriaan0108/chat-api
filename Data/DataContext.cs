using chat_api.Models;
using Microsoft.EntityFrameworkCore;

namespace chat_api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<ChatPerson> ChatPersons { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}