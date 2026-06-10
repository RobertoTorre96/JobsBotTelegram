using JobsBotApp.Data;
using JobsBotApp.features.GetOnBoard;
using JobsBotApp.features.GetOnBoard.Services;
using JobsBotApp.features.telegram.service;
using Microsoft.EntityFrameworkCore;

namespace JobsBotApp;

public class Worker : BackgroundService {
    private readonly ILogger<Worker> _logger;
    private readonly IJobProvider _jobProvider;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ITelegramService _telegramService;

    public Worker(
        ILogger<Worker> logger,
        IJobProvider jobProvider,
        IServiceScopeFactory scopeFactory,
        ITelegramService telegramService) {
        _logger = logger;
        _jobProvider = jobProvider;
        _scopeFactory = scopeFactory;
        _telegramService = telegramService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        await _telegramService.SendMessageAsync(
            "🚀 JobsBot iniciado correctamente");

        while (!stoppingToken.IsCancellationRequested) {
            try {
                await ProcessJobsAsync(stoppingToken);

                await Task.Delay(
                    TimeSpan.FromMinutes(5),
                    stoppingToken);

            } catch (Exception ex) {
                _logger.LogError(
                    ex,
                    "Error ejecutando JobsBot");

                await Task.Delay(
                    TimeSpan.FromSeconds(30),
                    stoppingToken);
            }
        }
    }

    private async Task ProcessJobsAsync(CancellationToken cancellationToken) {
        var jobs = await _jobProvider.GetJobsAsync();

        using var scope = _scopeFactory.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<JobsDbContext>();

        var newJobs = new List<JobOffer>();

        foreach (var job in jobs) {
            var exists = await db.JobOffers.AnyAsync(
                x => x.Source == job.Source &&
                     x.ExternalId == job.ExternalId,
                cancellationToken);

            if (exists) {
                continue;
            }

            db.JobOffers.Add(job);
            newJobs.Add(job);

            _logger.LogInformation(
                "Nuevo empleo detectado: {Title}",
                job.Title);
        }

        if (newJobs.Count == 0) {
            _logger.LogInformation("No se encontraron nuevos empleos.");
            return;
        }

        await db.SaveChangesAsync(cancellationToken);

        foreach (var job in newJobs) {
            await _telegramService.SendMessageAsync(
                $"""
                🚀 Nuevo empleo

                📌 {job.Title}

                🔗 {job.Url}
                """);
        }

        _logger.LogInformation(
            "Se guardaron y notificaron {Count} nuevos empleos.",
            newJobs.Count);
    }
}