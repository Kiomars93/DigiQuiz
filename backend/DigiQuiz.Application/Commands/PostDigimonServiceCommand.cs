using DigiQuiz.Application.Requests;
using DigiQuiz.Domain.Entities;
using DigiQuiz.Domain.Models;
using MediatR;

namespace DigiQuiz.Application.Commands;

public class PostDigimonServiceCommand : IRequest<Player>
{
    public readonly PostDigimonServiceRequest _serviceRequest;
    public PostDigimonServiceCommand(PostDigimonServiceRequest serviceRequest)
    {
        _serviceRequest = serviceRequest;
    }
}