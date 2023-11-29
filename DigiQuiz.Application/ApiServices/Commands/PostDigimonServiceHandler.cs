using DigiQuiz.Application.ApiServices.Requests;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Entities;

namespace DigiQuiz.Application.ApiServices.Commands
{
    public class PostDigimonServiceHandler : IPostDigimonServiceHandler
    {
        private readonly IPlayerRepository _playerRepository;

        public PostDigimonServiceHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // Todo: Byta ut alla PostDigimon namn till PostPlayer istället
        public async Task<Player> PostDigimonHandler(PostDigimonServiceRequest serviceRequest)
        {
            // Todo: 
            // Om det är rätt svar så ska vi skicka ner poäng till respektive spelare annars inget
            // Test scenario där poäng:en är rätt
            var player = new Player
            {
                Name = serviceRequest.Name,
                Points = serviceRequest.Points,
                GameDate = DateTime.Now
            };

            return await _playerRepository.AddPlayer(player);
        }
    }
}