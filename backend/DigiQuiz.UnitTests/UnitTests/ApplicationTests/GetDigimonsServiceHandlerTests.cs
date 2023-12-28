using DigiQuiz.Application.CQRS.Handlers;
using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Responses;
using DigiQuiz.Domain.Models;
using Moq;

namespace DigiQuiz.Tests.UnitTests.ApplicationTests;

public class GetDigimonsServiceHandlerTests
{
    [Fact]
    public async Task GetDigimons_ReceivingResult_HappyCase()
    {
        // Arrange
        var digimonRepository = new Mock<IDigimonRepository>();
        var query = new GetDigimonsServiceQuery();
        var handler = new GetDigimonsServiceHandler(digimonRepository.Object);

        var digimons = new Digimons
        {
            Contents = new List<ContentObj>
            {
                new ContentObj
                {
                    Id = 1,
                    Name = "Agumon",
                    Image = "https://www.digi-api.com/images/digimon/w/Agumon.png"
                },
                new ContentObj
                {
                    Id = 2,
                    Name = "Gabumon",
                    Image = "https://www.digi-api.com/images/digimon/w/Gabumon.png"
                },
                new ContentObj
                {
                    Id = 3,
                    Name = "Patamon",
                    Image = "https://www.digi-api.com/images/digimon/w/Patamon.png"
                }
            }
        };

        var expectedResponse = digimons.Contents.Select(d => new GetDigimonsServiceResponse
        {
            Id = d.Id,
            Name = d.Name,
            Image = d.Image
        })
        .ToList();

        digimonRepository.Setup(x => x.GetDigimons(It.IsAny<int>())).ReturnsAsync(digimons);

        // Act
        var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(expectedResponse, actual, new GetDigimonsServiceResponseComparer());
    }
    private class GetDigimonsServiceResponseComparer : IEqualityComparer<GetDigimonsServiceResponse>
    {
        public bool Equals(GetDigimonsServiceResponse x, GetDigimonsServiceResponse y)
        {
            return x.Id == y.Id && x.Name == y.Name && x.Image == y.Image;
        }

        public int GetHashCode(GetDigimonsServiceResponse obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.Image.GetHashCode();
        }
    }

    [Fact]
    public async Task GetDigimons_NullRepositoryResult_ShouldThrowsNullException()
    {
        // Arrange
        var digimonRepository = new Mock<IDigimonRepository>();
        var query = new GetDigimonsServiceQuery();
        var handler = new GetDigimonsServiceHandler(digimonRepository.Object);
        var expectedMessage = "The Digimon repository returned a null value. Please check the data source or repository implementation.";
        // Act
        var action = () => handler.Handle(query, It.IsAny<CancellationToken>());

        //assert
        var exception =  await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal(expectedMessage, exception.Message);
    }
}
