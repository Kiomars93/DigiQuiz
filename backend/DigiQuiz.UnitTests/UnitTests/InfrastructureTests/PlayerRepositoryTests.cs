using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;
using DigiQuiz.Infrastructure.Data.DbContexts;
using DigiQuiz.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace DigiQuiz.Tests.UnitTests.InfrastructureTests;

public class PlayerRepositoryTests
{
    [Fact]
    public async Task GetAll_ReceivingResult_HappyCase()
    {
        // Arrange
        //Todo: Fixa alla list IEnumerable etc
        var players = new List<Player>();

        var options = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase(databaseName: "Digiquiz")
            .Options;

        var playerDbContextMock = new Mock<PlayerDbContext>(options);
        var playerRepositoryMock = new Mock<IPlayerRepository>();
        IPlayerRepository playerRepository = new PlayerRepository(playerDbContextMock.Object);

        playerDbContextMock.Setup(x => x.Player).ReturnsDbSet(GetFakePlayers());
        playerRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(players);

        // Act
        var actual = await playerRepository.GetAll();

        players = actual.Select(x => new Player
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
            GameDate = x.GameDate
        }).ToList();

        // Assert
        Assert.Equal(players, actual, new PlayersComparer());
    }

    private static List<Player> GetFakePlayers()
    {
        return new List<Player>()
        {
            new Player
            {
                Id = 1,
                Name = "Player 5",
                Points = 5,
                GameDate = DateTime.Now
            },
            new Player
            {
                Id = 2,
                Name = "Player 20",
                Points = 15,
                GameDate = DateTime.Now
            }
        };
    }
    private class PlayersComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            return x.Id == y.Id && x.Name == y.Name && x.Points == y.Points && x.GameDate == y.GameDate;
        }

        public int GetHashCode(Player obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.Points.GetHashCode() ^ obj.GameDate.GetHashCode();
        }
    }
}
