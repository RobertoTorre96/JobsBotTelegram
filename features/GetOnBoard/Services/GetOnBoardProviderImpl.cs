using JobsBotApp.DTOs;
using System.Text.Json;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JobsBotApp.features.GetOnBoard.Services;

public class GetOnBoardProviderImpl : IJobProvider
{
    private readonly HttpClient _httpClient;
    private readonly string[] _keywords ={
            ".net",
            "c#",
            "backend",
            "asp.net",
            "python",
            "react",
            "developer",
            "fullstack",
        };

    public GetOnBoardProviderImpl(HttpClient httpClient){
        _httpClient = httpClient;
    }

    public async Task<List<JobOffer>> GetJobsAsync(){

        var url = "https://www.getonbrd.com/api/v0/search/jobs/?remote=true";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var data= JsonSerializer.Deserialize<GetOnBoardResponseDto>(json,new JsonSerializerOptions{
                                                                     PropertyNameCaseInsensitive = true});

        if (data == null) {
            return new List<JobOffer>();
        }

        var jobs = data.Data.Select(job => new JobOffer {
            ExternalId = job.Id,
            Source = "GetOnBoard",
            Title = job.Attributes.Title,
            Description = job.Attributes.Description,
            Remote = job.Attributes.Remote,
            Url = job.Links.PublicUrl,
            PublishedAt = DateTimeOffset.FromUnixTimeSeconds(job.Attributes.PublishedAt).DateTime,
            Sent = false
        }).Where(job =>
             _keywords.Any(k =>
                job.Title.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                job.Description.Contains(k, StringComparison.OrdinalIgnoreCase)
             )
        )
        .ToList();

        return jobs;


    }
}