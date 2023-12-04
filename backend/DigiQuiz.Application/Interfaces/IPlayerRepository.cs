using DigiQuiz.Domain.Entities;

namespace DigiQuiz.Application.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetAll();

    Task<Player> GetPlayerById(int playerId);

    Task<Player> AddPlayer(Player toCreate);

    Task<Player> UpdatePlayer(int playerId, string name, int points);

    Task DeletePlayer(int playerId);
}