using DigiQuiz.Application.Interfaces;
using DigiQuiz.Infrastructure.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace DigiQuiz.Tests.UnitTests.InfrastructureTests;

public class DigimonRepositoryTests
{
    [Fact]
    public async Task GetDigimons_ReceivingResult_CorrectNumberOfItems()
    {
        // Arrange
        var responseContent = @$"{{ ""content"": [
                                {{ ""id"": 1, ""name"": ""Agumon"", ""image"": ""https://digimon-api.com/images/digimon/w/Agumon.png"" }},
                                {{ ""id"": 2, ""name"": ""Greymon"", ""image"": ""https://digimon-api.com/images/digimon/w/Greymon.png"" }},
                                {{ ""id"": 3, ""name"": ""Ikkakumon"", ""image"": ""https://digimon-api.com/images/digimon/w/Ikkakumon.png"" }}
                                ] }}";

        HttpResponseMessage responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        };

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage)
            .Verifiable();

        var httpClient = new HttpClient(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://www.digi-api.com/api/v1")
        };

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);

        IDigimonRepository digimonRepository = new DigimonRepository(httpClientFactoryMock.Object);

        // Act
        var result = await digimonRepository.GetDigimons(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Contents.Count);

        httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_CorrectItemsTypesAndValues()
    {
        // Arrange
        var responseContent = @$"{{ ""content"": [
                                {{ ""id"": 1, ""name"": ""Agumon"", ""image"": ""https://digimon-api.com/images/digimon/w/Agumon.png"" }},
                                {{ ""id"": 2, ""name"": ""Greymon"", ""image"": ""https://digimon-api.com/images/digimon/w/Greymon.png"" }},
                                {{ ""id"": 3, ""name"": ""Ikkakumon"", ""image"": ""https://digimon-api.com/images/digimon/w/Ikkakumon.png"" }}
                                ] }}";

        HttpResponseMessage responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        };

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage)
            .Verifiable();

        var httpClient = new HttpClient(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://www.digi-api.com/api/v1")
        };

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);

        IDigimonRepository digimonRepository = new DigimonRepository(httpClientFactoryMock.Object);

        // Act
        var result = await digimonRepository.GetDigimons(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Contents[0].Id);
        Assert.Equal("Greymon", result.Contents[1].Name);
        Assert.Equal("https://digimon-api.com/images/digimon/w/Ikkakumon.png", result.Contents[2].Image);
        httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_InternalServerError()
    {
        // Arrange
        HttpResponseMessage responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"someProperty\": \"someValue\" }", Encoding.UTF8, "application/json")
        };

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage)
            .Verifiable();

        var httpClient = new HttpClient(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://www.digidigiddafdbafkdsbhk-api.com/api/v1")
        };

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);

        IDigimonRepository digimonRepository = new DigimonRepository(httpClientFactoryMock.Object);

        // Act
        var result = await Assert.ThrowsAsync<Exception>(() => digimonRepository.GetDigimons(1));

        // Assert
        Assert.Equal("The contents are missing!", result.Message);
        httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}