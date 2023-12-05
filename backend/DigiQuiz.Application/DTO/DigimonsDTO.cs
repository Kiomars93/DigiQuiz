namespace DigiQuiz.Application.DTO;

public class DigimonsDTO
{
    public List<ContentObjDTO> Contents { get; set; }
}

public class ContentObjDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}