using System.Text.Json.Serialization;

namespace JobsBotApp.DTOs;

public class GetOnBoardAttributesDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("remote")]
    public bool Remote { get; set; }

    [JsonPropertyName("published_at")]
    public long PublishedAt { get; set; }

    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; } = "";

    [JsonPropertyName("min_salary")]
    public int? MinSalary { get; set; }

    [JsonPropertyName("max_salary")]
    public int? MaxSalary { get; set; }
}