using CloudResourceOptimizer.API.Orchestration;
using CloudResourceOptimizer.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<AzureResourceService>();
builder.Services.AddSingleton<CostAnalysisService>();
builder.Services.AddSingleton<ActionExecutor>();
builder.Services.AddSingleton<AgenticAIOrchestrator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// API endpoints
app.MapGet("/", () => "Proactive Cloud Resource Optimization Assistant 🚀");

app.MapGet("/optimize", async (AgenticAIOrchestrator orchestrator) =>
{
    var result = await orchestrator.OptimizeResourcesAsync();
    return Results.Ok(result);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();