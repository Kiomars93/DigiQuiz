using DigiQuiz.Application.DTO;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Queries;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class GetDigimonsServiceHandler : IRequestHandler<GetDigimonsServiceQuery, DigimonsDTO>
{
    private readonly IDigimonRepository _digimonRepository;

    public GetDigimonsServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }
    public async Task<DigimonsDTO> Handle(GetDigimonsServiceQuery request, CancellationToken cancellationToken)
    {
        var random = new Random();
        var digimons = await _digimonRepository.GetDigimons(random.Next(48));

        var digimonsDTO = new DigimonsDTO
            {
                Contents = digimons.Contents.Select(contentObj => new ContentObjDTO
                {
                    Id = contentObj.Id,
                    Name = contentObj.Name,
                    Image = contentObj.Image
                }).ToList()
            };

        return digimonsDTO;
    }
}