namespace DigiQuiz.Application.Responses;

public class GetPlayerServiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public DateTime GameDate { get; set; }
}