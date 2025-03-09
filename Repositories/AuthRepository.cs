using chat_api.Data;
using chat_api.Interfaces.Repositories;
using chat_api.Models;
using Microsoft.EntityFrameworkCore;

namespace chat_api.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;

    public AuthRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<Person> GetPerson(string username, string secret)
    {
        return await _context.Persons.FirstOrDefaultAsync(p => p.Username == username && p.Secret == secret);
    }

    public async Task<Person> GetPersonById(int userId)
    {
        return await _context.Persons.FindAsync(userId);
    }

    public async Task<IEnumerable<Person>> GetAllPersons()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person> GetPersonByUsername(string username)
    {
        return await _context.Persons.FirstOrDefaultAsync(p => p.Username == username);
    }

    public async Task<Person> CreatePerson(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }
}