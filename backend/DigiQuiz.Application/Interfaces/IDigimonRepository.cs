using DigiQuiz.Domain.Models;

namespace DigiQuiz.Application.Interfaces;

public interface IDigimonRepository
{
    Task<Digimons> GetDigimons(int page);
}