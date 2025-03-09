using chat_api.Data;
using chat_api.Interfaces.Repositories;
using chat_api.Models;
using Microsoft.EntityFrameworkCore;

namespace chat_api.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _context;

    public MessageRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<Message> CreateMessage(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
    {
        return await _context.Messages.Where(m => m.ChatId == chatId).ToListAsync();
    }
}