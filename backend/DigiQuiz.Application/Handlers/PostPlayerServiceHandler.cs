using DigiQuiz.Application.Commands;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Models;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class PostPlayerServiceHandler : IRequestHandler<PostPlayerServiceCommand, Player>
{
    private readonly IPlayerRepository _playerRepository;

    public PostPlayerServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Player> Handle(PostPlayerServiceCommand serviceCommand, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Name = serviceCommand._serviceRequest.Name,
            Points = serviceCommand._serviceRequest.Points,
            GameDate = DateTime.Now
        };

        return await _playerRepository.AddPlayer(player);
    }
}