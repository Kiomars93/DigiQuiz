using DigiQuiz.Application.Requests;
using DigiQuiz.Application.Responses;
using MediatR;

namespace DigiQuiz.Application.CQRS.Commands;

public class PostPlayerServiceCommand : IRequest<PostPlayerServiceResponse>
{
    public readonly PostPlayerServiceRequest _serviceRequest;
    public PostPlayerServiceCommand(PostPlayerServiceRequest serviceRequest)
    {
        _serviceRequest = serviceRequest;
    }
}