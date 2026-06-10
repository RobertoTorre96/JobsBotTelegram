using System.Text.Json.Serialization;

namespace JobsBotApp.DTOs;

public class GetOnBoardLinksDto
{
    [JsonPropertyName("public_url")]
    public string PublicUrl { get; set; } = "";
}