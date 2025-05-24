using CloudResourceOptimizer.API.Orchestration;
using CloudResourceOptimizer.API.Services;
using Serilog;

// Configure Serilog from appsettings.json
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

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
app.UseSerilogRequestLogging();

try
{
    Log.Information("Starting CloudResourceOptimizer API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}