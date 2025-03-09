using chat_api.Dtos;
using chat_api.Models;

namespace chat_api.Interfaces.Services;

public interface IAuthService
{
    Task<Person> IsAuthorized(string projectId, string username, string secret);

    Task<Person> GetPersonById(int userId);

    Task<Person> CreatePerson(CreatePersonDto personDto);

    Task<Person> GetPersonByUsername(string username);

    Task<IEnumerable<Person>> GetAllPersons();
}