using chat_api.Models;

namespace chat_api.Interfaces.Repositories;

public interface IChatRepository
{
    Task<Chat> CreateChat(Chat chat);

    Task<ChatPerson> CreateChatPerson(ChatPerson chatPerson);
}