using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.Queries;

public class GetDigimonsServiceQuery : IRequest<List<GetDigimonsServiceResponse>>
{
}