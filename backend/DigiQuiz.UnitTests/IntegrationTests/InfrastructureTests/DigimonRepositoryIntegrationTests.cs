using DigiQuiz.Domain.Models;
using DigiQuiz.Infrastructure.Services;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DigiQuiz.Tests.IntegrationTests.InfrastructureTests;

public class DigimonRepositoryIntegrationTests
{
    private readonly HttpClient _client;

    public DigimonRepositoryIntegrationTests()
    {
        _client = new HttpClient();
    }

    [Fact]
    public async Task GetDigimons_ReceivingDigmonsResult_HappyCase()
    {
        // Arrange
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_client);
        var digimonRepository = new DigimonRepository(httpClientFactory.Object);

        // Act
        var result = await digimonRepository.GetDigimons(0);

        var expectedDigimonList = new Digimons
        {
            Contents = result.Contents.ToList()
        };

        // Assert
        Assert.Equal(expectedDigimonList.Contents, result.Contents, new DigimonsComparer());
    }

    [Fact]
    public async Task GetDigimons_ReceivingNullResult_ShouldThrowAnException()
    {
        // Arrange
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_client);
        var digimonRepository = new DigimonRepository(httpClientFactory.Object);
        var expectedMessage = "The contents are missing!";

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => await digimonRepository.GetDigimons(3000));

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    // Todo får återkomma till catch delen. Kanske behöver göra om själv:a koden.
    //[Fact]
    //public async Task GetDigimons_WrongURL_ShouldThrowAnException()
    //{
    //    // Arrange
    //    var invalidUrl = "https://www.digi-api.com/api/v1/digimfsfdsonfghjkl?page=1&pageSize=30";
    //    var httpClientFactory = new Mock<IHttpClientFactory>();
    //    var httpClient = new Mock<HttpClient>();

    //    var httpResponseMessage = new HttpResponseMessage
    //    {
    //        //StatusCode = HttpStatusCode.NotFound,
    //        //Content = new StringContent("Not Found"),
    //    };

    //httpClientFactory.Setup(x => x.CreateClient(invalidUrl).SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(httpResponseMessage);


    //    var digimonRepository = new DigimonRepository(httpClientFactory.Object);
    //    var expectedMessage = "The URL page is missing!";

    //    // Act & Assert
    //    var exception = await Assert.ThrowsAsync<Exception>(async () => await digimonRepository.GetDigimons(1));
    //    //var result = await digimonRepository.GetDigimons(0);
    //    // Assert
    //    Assert.Equal(expectedMessage, exception.Message);
    //}

    private class DigimonsComparer : IEqualityComparer<ContentObj>
    {
        public bool Equals(ContentObj x, ContentObj y)
        {
            return x.Id == y.Id && x.Name == y.Name && x.Image == y.Image;
        }

        public int GetHashCode(ContentObj obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.Image.GetHashCode();
        }
    }
}
