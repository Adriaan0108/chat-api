using chat_api.Models;

namespace chat_api.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<Person> GetPerson(string username, string secret);

    Task<Person> CreatePerson(Person person);

    Task<Person> GetPersonById(int userId);

    Task<Person> GetPersonByUsername(string username);

    Task<IEnumerable<Person>> GetAllPersons();
}