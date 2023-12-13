using DigiQuiz.Application.DTO;
using MediatR;

namespace DigiQuiz.Application.Queries
{
    public class GetPlayersServiceQuery: IRequest<List<PlayerDTO>>
    {
    }
}