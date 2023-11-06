using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Entities;
using DigiQuiz.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DigiQuiz.Infrastructure.Repositories;

internal class PlayerRepository : IPlayerRepository
{
    private readonly PlayerDbContext _dbContext;
    public PlayerRepository(PlayerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player> AddPlayer(Player toCreate)
    {
        _dbContext.Player.Add(toCreate);

        await _dbContext.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeletePlayer(int playerId)
    {
        var player = _dbContext.Player
        .FirstOrDefault(p => p.Id == playerId);

        if (player is null) return;

        _dbContext.Player.Remove(player);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Player>> GetAll()
    {
        return await _dbContext.Player.ToListAsync();
    }

    public async Task<Player> GetPlayerById(int playerId)
    {
        return await _dbContext.Player.FirstOrDefaultAsync(p => p.Id == playerId);
    }

    public async Task<Player> UpdatePlayer(int playerId, string name, int points)
    {
        var person = await _dbContext.Player.FirstOrDefaultAsync(p => p.Id == playerId);
        person.Name = name;
        person.Points = points;

        await _dbContext.SaveChangesAsync();

        return person;
    }
}