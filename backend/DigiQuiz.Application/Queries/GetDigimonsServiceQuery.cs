using DigiQuiz.Domain.Models;
using MediatR;

namespace DigiQuiz.Application.Queries;

public class GetDigimonsServiceQuery : IRequest<Digimons>
{
}