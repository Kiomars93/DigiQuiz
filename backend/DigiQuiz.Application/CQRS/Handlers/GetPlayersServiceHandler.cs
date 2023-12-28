using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.CQRS.Handlers;

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
        
        if (response == null)
        {
            throw new Exception("The Player repository returned a null value. Please check the data source or repository implementation.");
        }
        
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