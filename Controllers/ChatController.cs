using chat_api.Dtos;
using chat_api.Interfaces.Services;
using chat_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace chat_api.Controllers;

[Route("/chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;

    public ChatController(IChatService chatService, MessageService messageService)
    {
        _chatService = chatService;
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto)
    {
        await _chatService.CreateChat(createChatDto);

        return Ok(new { message = "Chat created successfully." });
    }

    [HttpPost("/{chatId}/messages")]
    public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto createMessageDto, int chatId)
    {
        var userId = (int)HttpContext.Items["UserId"];

        await _messageService.CreateMessage(userId, chatId, createMessageDto.Text);

        return Ok(new { message = "Message created successfully." });
    }

    [HttpGet("/{chatId}/messages")]
    public async Task<IActionResult> GetChatMessages(int chatId)
    {
        var messages = await _messageService.GetChatMessages(chatId);

        return Ok(messages);
    }
}