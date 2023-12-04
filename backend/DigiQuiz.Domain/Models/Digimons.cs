using System.Text.Json.Serialization;

namespace DigiQuiz.Domain.Models;

public class Digimons
{
    [JsonPropertyName("content")]
    public List<ContentObj> Contents { get; set; }
}

public class ContentObj
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
}