using DigiQuiz.Application.Commands;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Entities;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class PostDigimonServiceHandler : IRequestHandler<PostDigimonServiceCommand, Player>
{
    private readonly IPlayerRepository _playerRepository;

    public PostDigimonServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Player> Handle(PostDigimonServiceCommand serviceCommand, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Name = serviceCommand._serviceRequest.Name,
            Points = 5,
            GameDate = DateTime.Now
        };

        return await _playerRepository.AddPlayer(player);
    }
}