using DigiQuiz.Domain.Models;

namespace DigiQuiz.Application.Interfaces;

public interface IGetDigimonsServiceHandler
{
    Task<Digimons> GetDigimonsHandler();
}
