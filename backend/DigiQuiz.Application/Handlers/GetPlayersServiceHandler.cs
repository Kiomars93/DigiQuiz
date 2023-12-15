using DigiQuiz.Application.DTO;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Queries;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class GetPlayersServiceHandler : IRequestHandler<GetPlayersServiceQuery, List<PlayerDTO>>
{
    private readonly IPlayerRepository _playerRepository;
    public GetPlayersServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<PlayerDTO>> Handle(GetPlayersServiceQuery request, CancellationToken cancellationToken)
    {
        var response = await _playerRepository.GetAll();

        var playerList = response.Select(x => new PlayerDTO
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
            GameDate = x.GameDate
        }).OrderByDescending(x => x.Points).Take(5).ToList();

        return playerList;
    }
}