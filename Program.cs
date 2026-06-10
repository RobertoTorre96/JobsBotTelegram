using JobsBotApp;
using JobsBotApp.Data;
using JobsBotApp.features.GetOnBoard.Services;
using JobsBotApp.features.telegram.service;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IJobProvider, GetOnBoardProviderImpl>();
builder.Services.AddTransient<ITelegramService, TelegramServiceImpl>();

builder.Services.AddDbContext<JobsDbContext>(options =>
    options.UseSqlite("Data Source=jobs.db"));

var host = builder.Build();
host.Run();
