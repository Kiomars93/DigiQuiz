using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;

namespace DigiQuiz.Application.ApiServices.Queries;

public class GetDigimonsServiceHandler
{
    private readonly IDigimonRepository _digimonRepository;

    public GetDigimonsServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }

    public async Task<Digimons> GetDigimons()
    {
        return await _digimonRepository.GetDigimons();
    }
}