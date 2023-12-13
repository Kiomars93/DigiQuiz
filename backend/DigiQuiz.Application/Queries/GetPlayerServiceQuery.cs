using DigiQuiz.Application.DTO;
using MediatR;

namespace DigiQuiz.Application.Queries
{
    public class GetPlayerServiceQuery: IRequest<List<PlayerDTO>>
    {
    }
}