using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Queries;
using DigiQuiz.Domain.Models;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class GetDigimonsServiceHandler : IRequestHandler<GetDigimonsServiceQuery, Digimons>
{
    private readonly IDigimonRepository _digimonRepository;

    public GetDigimonsServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }
    public async Task<Digimons> Handle(GetDigimonsServiceQuery request, CancellationToken cancellationToken)
    {
        var random = new Random();
        return await _digimonRepository.GetDigimons(random.Next(285));
    }
}