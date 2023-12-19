using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.Queries
{
    public class GetPlayersServiceQuery: IRequest<List<GetPlayersServiceResponse>>
    {
    }
}