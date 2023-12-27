using DigiQuiz.Application.CQRS.Commands;
using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Requests;
using DigiQuiz.Application.Responses;
using DigiQuiz.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigiQuiz.UnitTests.UnitTests.ControllerTests;

public class DigimonControllerTests
{
    [Fact]
    public async Task GetDigimons_ReceivingResult_HappyCase()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var controller = new DigimonController(senderMock.Object);

        var expectedResponse = new List<GetDigimonsServiceResponse>
        {
            new GetDigimonsServiceResponse { Id = 1, Name = "Agumon", Image = "https://www.digi-api.com/images/digimon/w/Agumon.png"},
            new GetDigimonsServiceResponse { Id = 2, Name = "Gabumon", Image = "https://www.digi-api.com/images/digimon/w/Gabumon.png"},
            new GetDigimonsServiceResponse { Id = 3, Name = "Greymon", Image = "https://www.digi-api.com/images/digimon/w/Greymon.png"}
        };

        senderMock.Setup(x => x.Send(It.IsAny<GetDigimonsServiceQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse)
                    .Verifiable();

        // Act
        var result = await controller.GetDigimons();

        // Assert
        var okResult = Assert.IsType<ActionResult<List<GetDigimonsServiceResponse>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var actual = Assert.IsAssignableFrom<List<GetDigimonsServiceResponse>>(okObjectResult.Value);

        Assert.NotNull(okObjectResult.Value);
        Assert.Equal(3, actual.Count);
        Assert.Equal(expectedResponse, actual);
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_NotFoundObjectResult()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var controller = new DigimonController(senderMock.Object);

        var expectedResponse = new List<GetDigimonsServiceResponse>();
        senderMock.Setup(x => x.Send(It.IsNotIn<GetDigimonsServiceQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

        // Act
        var result = await controller.GetDigimons();

        // Assert
        var notFound = Assert.IsType<ActionResult<List<GetDigimonsServiceResponse>>>(result);
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(notFound.Result);
        var actual = Assert.IsAssignableFrom<List<GetDigimonsServiceResponse>>(notFoundObjectResult.Value);

        Assert.Null(notFound.Value);
        Assert.Empty(actual);
        Assert.Equal(expectedResponse, actual);
    }

    [Fact]
    public async Task PostPlayer_CreatingRequest_HappyCase()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var controller = new DigimonController(senderMock.Object);

        var expectedResponse = new PostPlayerServiceResponse
        {
            Name = "Best Player",
            Points = 100
        };

        senderMock.Setup(x => x.Send(It.IsAny<PostPlayerServiceCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse)
                    .Verifiable();

        // Act
        var result = await controller.PostPlayer(new PostPlayerServiceRequest());

        // Assert
        var okResult = Assert.IsType<ActionResult<PostPlayerServiceResponse>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var actual = Assert.IsAssignableFrom<PostPlayerServiceResponse>(okObjectResult.Value);

        Assert.NotNull(okObjectResult.Value);
        Assert.Equal(expectedResponse, actual);
    }

    [Fact]
    public async Task GetPlayers_ReceivingPlayers_HappyCase()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var controller = new DigimonController(senderMock.Object);

        var expectedResponse = new List<GetPlayersServiceResponse>
        {
            new GetPlayersServiceResponse { Id = 1, Name = "Player 1", Points = 10 },
            new GetPlayersServiceResponse { Id = 2, Name = "Player 2", Points = 20 },
            new GetPlayersServiceResponse { Id = 3, Name = "Player 3", Points = 300 }
        };

        senderMock.Setup(x => x.Send(It.IsAny<GetPlayersServiceQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse)
                    .Verifiable();

        // Act
        var result = await controller.GetPlayers();

        // Assert
        var okResult = Assert.IsType<ActionResult<List<GetPlayersServiceResponse>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var actual = Assert.IsAssignableFrom<List<GetPlayersServiceResponse>>(okObjectResult.Value);

        Assert.NotNull(okObjectResult.Value);
        Assert.Equal(3, actual.Count);
        Assert.Equal(expectedResponse, actual);
    }

    [Fact]
    public async Task GetPlayers_ReceivingPlayers_NotFoundObjectResult()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var controller = new DigimonController(senderMock.Object);

        var expectedResponse = new List<GetPlayersServiceResponse>();

        senderMock.Setup(x => x.Send(It.IsNotIn<GetPlayersServiceQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse)
                    .Verifiable();

        // Act
        var result = await controller.GetPlayers();

        // Assert
        var notFound = Assert.IsType<ActionResult<List<GetPlayersServiceResponse>>>(result);
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(notFound.Result);
        var actual = Assert.IsAssignableFrom<List<GetPlayersServiceResponse>>(notFoundObjectResult.Value);

        Assert.Null(notFound.Value);
        Assert.Empty(actual);
        Assert.Equal(expectedResponse, actual);
    }
}