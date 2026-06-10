namespace JobsBotApp.DTOs;

public class GetOnBoardJobDto
{
    public string Id { get; set; } = "";

    public GetOnBoardAttributesDto Attributes { get; set; } = new();

    public GetOnBoardLinksDto Links { get; set; } = new();
}