using DigiQuiz.Application.Requests;
using DigiQuiz.Domain.Entities;
using MediatR;

namespace DigiQuiz.Application.Commands;

public class PostPlayerServiceCommand : IRequest<Player>
{
    public readonly PostPlayerServiceRequest _serviceRequest;
    public PostPlayerServiceCommand(PostPlayerServiceRequest serviceRequest)
    {
        _serviceRequest = serviceRequest;
    }
}