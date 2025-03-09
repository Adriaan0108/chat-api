using chat_api.Data;
using chat_api.Interfaces.Repositories;
using chat_api.Models;

namespace chat_api.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;

    public ChatRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Chat> CreateChat(Chat chat)
    {
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
        return chat;
    }

    public async Task<ChatPerson> CreateChatPerson(ChatPerson chatPerson)
    {
        await _context.ChatPersons.AddAsync(chatPerson);
        await _context.SaveChangesAsync();
        return chatPerson;
    }
}