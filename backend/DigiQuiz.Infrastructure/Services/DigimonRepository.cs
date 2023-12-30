using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;
using System.Text.Json;


namespace DigiQuiz.Infrastructure.Services;

public class DigimonRepository : IDigimonRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl = "https://www.digi-api.com/api/v1/";
    public DigimonRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Digimons> GetDigimons(int page)
    {
        var httpResponse = await _httpClientFactory.CreateClient().GetAsync($"{baseUrl}digimon?page={page}&pageSize=30");

        try
        {
            httpResponse.EnsureSuccessStatusCode();
            var jsonStringResult = await httpResponse.Content.ReadAsStringAsync();
            var digimons = JsonSerializer.Deserialize<Digimons>(jsonStringResult);

            if (digimons.Contents == null)
            {
                throw new Exception("The contents are missing!");
            }

            return digimons;
        }
        catch (HttpRequestException ex)
        {
            // Todo: Create custom error
            throw new Exception(ex.Message + "\n page is missing!");
        }
    }
}