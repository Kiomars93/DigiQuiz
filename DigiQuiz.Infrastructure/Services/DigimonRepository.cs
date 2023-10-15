using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;
using System.Text.Json;

namespace DigiQuiz.Infrastructure.Services;

public class DigimonRepository : IDigimonRepository
{
    private readonly HttpClient _httpClient = new();
    public DigimonRepository()
    {
    }

    public async Task<Digimons> GetDigimons()
    {
        _httpClient.BaseAddress = new Uri("https://www.digi-api.com/api/v1/");

        var digimons = new Digimons();

        var httpResponse = await _httpClient.GetAsync("digimon?page=0");
        httpResponse.EnsureSuccessStatusCode();
        var jsonStringResult = await httpResponse.Content.ReadAsStringAsync();
        digimons = JsonSerializer.Deserialize<Digimons>(jsonStringResult);
        return digimons;
    }
}
