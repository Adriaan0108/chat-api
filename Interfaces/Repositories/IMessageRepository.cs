using chat_api.Models;

namespace chat_api.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<Message> CreateMessage(Message message);

    Task<IEnumerable<Message>> GetChatMessages(int chatId);
}