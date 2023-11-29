using DigiQuiz.Application.ApiServices.Requests;
using DigiQuiz.Domain.Entities;

namespace DigiQuiz.Application.Interfaces
{
    public interface IPostDigimonServiceHandler
    {
        Task<Player> PostDigimonHandler(PostDigimonServiceRequest serviceRequest);
    }
}
