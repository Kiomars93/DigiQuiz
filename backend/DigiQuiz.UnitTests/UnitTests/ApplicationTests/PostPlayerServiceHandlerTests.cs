using DigiQuiz.Application.CQRS.Commands;
using DigiQuiz.Application.CQRS.Handlers;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Requests;
using DigiQuiz.Application.Responses;
using DigiQuiz.Domain.Models;
using Moq;

namespace DigiQuiz.Tests.UnitTests.ApplicationTests;

public class PostPlayerServiceHandlerTests
{
    [Fact]
    public async Task PostPlayer_AddingPlayer_HappyCase()
    {
        // Arrange
        var playerRepository = new Mock<IPlayerRepository>();
        
        var request = new PostPlayerServiceRequest
        {
            Name = "Best Player",
            Points = 500
        };

        var command = new PostPlayerServiceCommand(request);
        var handler = new PostPlayerServiceHandler(playerRepository.Object);

        var player = new Player
        {
            Id = 1,
            Name = "Best Player",
            Points = 500,
            GameDate = DateTime.Now
        };

        var expectedResponse = new PostPlayerServiceResponse
        {
            Name = player.Name,
            Points = player.Points,
            GameDate = player.GameDate
        };

        playerRepository.Setup(x => x.AddPlayer(It.IsAny<Player>())).ReturnsAsync(player);

        // Act
        var actual = await handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(expectedResponse, actual, new PostPlayersServiceResponseComparer());
    }

    private class PostPlayersServiceResponseComparer : IEqualityComparer<PostPlayerServiceResponse>
    {
        public bool Equals(PostPlayerServiceResponse actualPlayer, PostPlayerServiceResponse expectedPlayer)
        {
            return actualPlayer.Name == expectedPlayer.Name
                && actualPlayer.Points == expectedPlayer.Points
                && actualPlayer.GameDate == expectedPlayer.GameDate;
        }

        public int GetHashCode(PostPlayerServiceResponse obj)
        {
            return obj.Name.GetHashCode() ^ obj.Points.GetHashCode() ^ obj.GameDate.GetHashCode();
        }
    }

    [Fact]
    public async Task PostPlayer_NullRepositoryResult_ShouldThrowsNullException()
    {
        // Arrange
        var playerRepository = new Mock<IPlayerRepository>();

        var request = new PostPlayerServiceRequest();
        var command = new PostPlayerServiceCommand(request);
        var handler = new PostPlayerServiceHandler(playerRepository.Object);
        var expectedMessage = "player was not successfully added to the repository.";

        // Act
        var action = () => handler.Handle(command, It.IsAny<CancellationToken>());

        //assert
        var exception = await Assert.ThrowsAsync<NullReferenceException>(action);
        Assert.Equal(expectedMessage, exception.Message);
    }
}
