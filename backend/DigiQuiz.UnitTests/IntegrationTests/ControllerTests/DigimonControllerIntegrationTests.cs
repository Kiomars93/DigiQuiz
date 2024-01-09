using DigiQuiz.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Text.Json;

namespace DigiQuiz.Tests.IntegrationTests.ControllerTests;

public class DigimonControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    // Todo: mock:a ISender för att testa fokusera på själva endpoints anropen
    private readonly Mock<ISender> _mockSender;

    public DigimonControllerIntegrationTests(WebApplicationFactory<Program> applicationFactory)
    {
        _client = applicationFactory.CreateClient();
        _mockSender = new Mock<ISender>();
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_ReturningCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/Digimon/Questions");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_HappyCase()
    {
        // Act
        var response = await _client.GetAsync("/api/Digimon/Questions");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDigimons_ReceivingResult_ShouldReturnListOfDigimons()
    {
        // Act
        var response = await _client.GetAsync("/api/Digimon/Questions");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        var actualResult = JsonSerializer.Deserialize<List<GetDigimonsServiceResponse>>(result);
        Assert.NotNull(actualResult);
        const int minimumExpectedDigimonCount = 12;
        Assert.True(actualResult.Count >= minimumExpectedDigimonCount, $"Expected at least {minimumExpectedDigimonCount} items, but got {actualResult.Count}");
        Assert.True(!string.IsNullOrEmpty(result));
    }

    [Fact]
    public async Task GetPlayers_ReceivingResult_ShouldReturnListOfPlayers()
    {
        // Act
        var response = await _client.GetAsync("/api/Digimon/Leaderboard");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        var actualResult = JsonSerializer.Deserialize<List<GetPlayersServiceResponse>>(result);
        Assert.NotNull(actualResult);
        Assert.True(!string.IsNullOrEmpty(result));
    }
}