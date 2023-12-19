using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Queries;
using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class GetPlayersServiceHandler : IRequestHandler<GetPlayersServiceQuery, List<GetPlayersServiceResponse>>
{
    private readonly IPlayerRepository _playerRepository;
    public GetPlayersServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<GetPlayersServiceResponse>> Handle(GetPlayersServiceQuery request, CancellationToken cancellationToken)
    {
        var response = await _playerRepository.GetAll();

        var playerServiceReponse = response.Select(x => new GetPlayersServiceResponse
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
            GameDate = x.GameDate
        })
            .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GameDate)
            .Take(5)
            .ToList();

        return playerServiceReponse;
    }
}