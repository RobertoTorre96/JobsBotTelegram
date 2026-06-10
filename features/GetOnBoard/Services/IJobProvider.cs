namespace JobsBotApp.features.GetOnBoard.Services;

public interface IJobProvider
{
    Task<List<JobOffer>> GetJobsAsync();
}