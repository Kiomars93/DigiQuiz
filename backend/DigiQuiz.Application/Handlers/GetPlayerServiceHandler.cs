using DigiQuiz.Application.DTO;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Queries;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class GetPlayerServiceHandler : IRequestHandler<GetPlayerServiceQuery, List<PlayerDTO>>
{
    private readonly IPlayerRepository _playerRepository;
    public GetPlayerServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<PlayerDTO>> Handle(GetPlayerServiceQuery request, CancellationToken cancellationToken)
    {
        var response = await _playerRepository.GetAll();

        var playerList = response.Select(x => new PlayerDTO
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
            GameDate = x.GameDate
        }).OrderByDescending(x => x.Points).ToList();

        return playerList;
    }
}