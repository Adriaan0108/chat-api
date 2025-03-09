using chat_api.Exceptions;
using chat_api.Interfaces.Repositories;
using chat_api.Interfaces.Services;
using chat_api.Models;

namespace chat_api.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }


    public async Task CreateMessage(int userId, int chatId, string text)
    {
        var message = new Message
        {
            SenderId = userId,
            ChatId = chatId,
            Text = text
        };

        await _messageRepository.CreateMessage(message);
    }

    public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
    {
        var messages = await _messageRepository.GetChatMessages(chatId);

        if (!messages.Any()) throw new CustomException(ErrorType.NotFound, "No Messages found for chat.");

        return messages;
    }
}