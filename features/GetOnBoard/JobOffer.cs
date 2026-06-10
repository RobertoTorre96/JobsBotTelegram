namespace JobsBotApp.features.GetOnBoard;

public class JobOffer{
    public int Id { get; set; }

    public string ExternalId { get; set; } = "";

    public string Source { get; set; } = "";

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public bool Remote { get; set; }

    public string Url { get; set; } = "";

    public DateTime PublishedAt { get; set; }

    public bool Sent { get; set; }
}