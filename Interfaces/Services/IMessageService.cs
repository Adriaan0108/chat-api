using chat_api.Models;

namespace chat_api.Interfaces.Services;

public interface IMessageService
{
    Task CreateMessage(int userId, int chatId, string text);

    Task<IEnumerable<Message>> GetChatMessages(int chatId);
}