using DigiQuiz.Application.Commands;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Responses;
using DigiQuiz.Domain.Models;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class PostPlayerServiceHandler : IRequestHandler<PostPlayerServiceCommand, PostPlayerServiceResponse>
{
    private readonly IPlayerRepository _playerRepository;

    public PostPlayerServiceHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PostPlayerServiceResponse> Handle(PostPlayerServiceCommand serviceCommand, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Name = serviceCommand._serviceRequest.Name,
            Points = serviceCommand._serviceRequest.Points,
            GameDate = DateTime.Now
        };

        var addedPlayer = await _playerRepository.AddPlayer(player);

        var postPlayerServiceResponse = new PostPlayerServiceResponse
        {
            Name = addedPlayer.Name,
            Points = addedPlayer.Points,
            GameDate = DateTime.Now
        };

        return postPlayerServiceResponse;
    }
}