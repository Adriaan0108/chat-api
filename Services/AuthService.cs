using chat_api.Dtos;
using chat_api.Exceptions;
using chat_api.Helpers;
using chat_api.Interfaces.Repositories;
using chat_api.Interfaces.Services;
using chat_api.Models;
using Microsoft.Extensions.Options;

namespace chat_api.Services;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly IAuthRepository _authRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IAuthRepository authRepository, IOptions<AppSettings> appSettings,
        IHttpContextAccessor httpContextAccessor)
    {
        _authRepository = authRepository;
        _appSettings = appSettings.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Person> IsAuthorized(string projectId, string username, string secret)
    {
        if (_appSettings.ProjectId != projectId)
            throw new CustomException(ErrorType.Unauthorized, "Invalid credentials.");

        var user = await _authRepository.GetPerson(username, secret);

        if (user == null) throw new CustomException(ErrorType.Unauthorized, "Invalid credentials.");

        return user;
    }

    public async Task<Person> GetPersonById(int userId)
    {
        var user = await _authRepository.GetPersonById(userId);

        if (user == null) throw new CustomException(ErrorType.NotFound, "User not found.");

        return user;
    }

    public async Task<Person> GetPersonByUsername(string username)
    {
        var user = await _authRepository.GetPersonByUsername(username);

        if (user == null) throw new CustomException(ErrorType.NotFound, "User not found.");

        return user;
    }

    public async Task<IEnumerable<Person>> GetAllPersons()
    {
        var persons = await _authRepository.GetAllPersons();

        if (!persons.Any()) throw new CustomException(ErrorType.NotFound, "No users found.");

        return persons;
    }

    public async Task<Person> CreatePerson(CreatePersonDto personDto)
    {
        var person = new Person
        {
            Username = personDto.Username, FirstName = personDto.FirstName, LastName = personDto.LastName,
            Secret = personDto.Secret
        };

        var existingPerson = await _authRepository.GetPersonByUsername(person.Username);

        if (existingPerson != null) throw new CustomException(ErrorType.Conflict, "Username already exists .");

        return await _authRepository.CreatePerson(person);
    }
}