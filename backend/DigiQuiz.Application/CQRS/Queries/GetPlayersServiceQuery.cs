using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.CQRS.Queries
{
    public class GetPlayersServiceQuery : IRequest<List<GetPlayersServiceResponse>>
    {
    }
}