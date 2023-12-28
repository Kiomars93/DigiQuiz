using DigiQuiz.Application.CQRS.Handlers;
using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Responses;
using DigiQuiz.Domain.Models;
using Moq;

namespace DigiQuiz.Tests.UnitTests.ApplicationTests;

public class GetPlayersServiceHandlerTests
{
    [Fact]
    public async Task GetPlayers_ReceivingResult_HappyCase()
    {
        // Arrange
        var playerRepository = new Mock<IPlayerRepository>();
        var query = new GetPlayersServiceQuery();
        var handler = new GetPlayersServiceHandler(playerRepository.Object);

        var playerList = new List<Player>
            {
                new Player
                {
                    Id = 1,
                    Name = "Player 1",
                    Points = 5,
                    GameDate = DateTime.Now
                },
                new Player
                {
                    Id = 2,
                    Name = "Player 2",
                    Points = 10,
                    GameDate = DateTime.Now
                },
                new Player
                {
                    Id = 3,
                    Name = "Player 3",
                    Points = 20,
                    GameDate = DateTime.Now
                }
            }.OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GameDate)
            .Take(5)
            .ToList();

        var expectedResponse = playerList.Select(p => new GetPlayersServiceResponse
        {
            Id = p.Id,
            Name = p.Name,
            Points = p.Points,
            GameDate = p.GameDate
        })
        .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GameDate)
            .Take(5)
            .ToList();

        playerRepository.Setup(x => x.GetAll()).ReturnsAsync(playerList);

        // Act
        var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(expectedResponse, actual, new GetPlayersServiceResponseComparer());
    }
    
    private class GetPlayersServiceResponseComparer : IEqualityComparer<GetPlayersServiceResponse>
    {
        public bool Equals(GetPlayersServiceResponse actualPlayer, GetPlayersServiceResponse expectedPlayer)
        {
            return actualPlayer.Id == expectedPlayer.Id 
                && actualPlayer.Name == expectedPlayer.Name 
                && actualPlayer.Points == expectedPlayer.Points 
                && actualPlayer.GameDate == expectedPlayer.GameDate;
        }

        public int GetHashCode(GetPlayersServiceResponse obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.Points.GetHashCode() ^ obj.GameDate.GetHashCode();
        }
    }

    [Fact]
    public async Task GetPlayers_NullRepositoryResult_ShouldThrowsNullException()
    {
        // Arrange
        var playerRepository = new Mock<IPlayerRepository>();
        var query = new GetPlayersServiceQuery();
        var handler = new GetPlayersServiceHandler(playerRepository.Object);
        var expectedMessage = "The Player repository returned a null value. Please check the data source or repository implementation.";

        // Act
        var action = () => handler.Handle(query, It.IsAny<CancellationToken>());

        //assert
        var exception = await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal(expectedMessage, exception.Message);
    }
}
