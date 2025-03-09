using chat_api.Dtos;
using chat_api.Models;

namespace chat_api.Interfaces.Services;

public interface IChatService
{
    Task<Chat> CreateChat(CreateChatDto chatDto);

    Task<ChatPerson> CreateChatPerson(int chatId, int personId);
}