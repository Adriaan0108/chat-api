using chat_api.Dtos;
using chat_api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chat_api.Controllers;

[Route("/users")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("/me")]
    public async Task<IActionResult> GetUser()
    {
        var userId = (int)HttpContext.Items["UserId"];

        var user = await _authService.GetPersonById(userId);

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _authService.GetAllPersons();

        return Ok(users);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonDto personDto)
    {
        await _authService.CreatePerson(personDto);

        return Ok(new { message = "User signed up successfully." });
    }
}