using DigiQuiz.Application.ApiServices.Requests;

namespace DigiQuiz.Application.Interfaces
{
    public interface IPostDigimonServiceHandler
    {
        Task<string> PostDigimonHandler(PostDigimonServiceRequest serviceRequest);
    }
}
