using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;

namespace DigiQuiz.Application.ApiServices.Queries;

public class GetDigimonsServiceHandler : IGetDigimonsServiceHandler
{
    private readonly IDigimonRepository _digimonRepository;

    public GetDigimonsServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }

    public async Task<Digimons> GetDigimonsHandler()
    {
        //Todo: när jag väl cache detta så vill jag inte att samma page ska poppas upp
        var random = new Random();
        return await _digimonRepository.GetDigimons(random.Next(285));
    }
}