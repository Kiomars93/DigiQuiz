using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.CQRS.Handlers;

public class GetDigimonsServiceHandler : IRequestHandler<GetDigimonsServiceQuery, List<GetDigimonsServiceResponse>>
{
    private readonly IDigimonRepository _digimonRepository;

    public GetDigimonsServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }
    public async Task<List<GetDigimonsServiceResponse>> Handle(GetDigimonsServiceQuery request, CancellationToken cancellationToken)
    {
        var digimons = await _digimonRepository.GetDigimons(new Random().Next(48));
        var getDigimonsServiceReponse = digimons.Contents.Select(c => new GetDigimonsServiceResponse { Id = c.Id, Name = c.Name, Image = c.Image }).ToList();

        return getDigimonsServiceReponse;
    }
}