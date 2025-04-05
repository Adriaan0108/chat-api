using chat_api.Dtos;
using chat_api.Interfaces.Repositories;
using chat_api.Interfaces.Services;
using chat_api.Models;

namespace chat_api.Services;

public class ChatService : IChatService
{
    private readonly IAuthService _authService;
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository, IAuthService authService)
    {
        _chatRepository = chatRepository;
        _authService = authService;
    }

    public async Task<ChatPerson> CreateChatPerson(int chatId, int personId)
    {
        var person = new ChatPerson
            { ChatId = chatId, PersonId = personId };

        var createdPerson = await _chatRepository.CreateChatPerson(person);

        return createdPerson;
    }


    public async Task<Chat> CreateChat(CreateChatDto chatDto)
    {
        var chat = new Chat
        {
            Title = chatDto.Title,
            IsDirectChat = chatDto.IsDirectChat
        };

        var createdChat = await _chatRepository.CreateChat(chat);

        foreach (var username in chatDto.Usernames)
        {
            var user = await _authService.GetPersonByUsername(username);

            await CreateChatPerson(createdChat.Id, user.Id);
        }

        return createdChat;
    }
}