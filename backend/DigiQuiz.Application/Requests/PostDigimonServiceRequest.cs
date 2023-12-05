using DigiQuiz.Application.DTO;

namespace DigiQuiz.Application.Requests;

public class PostDigimonServiceRequest
{
    public string DigimonName { get; set; }
    public int Points { get; set; }
    public List<DigimonsDTO> DigimonsQuestions { get; set; }
}
