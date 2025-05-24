using CloudResourceOptimizer.API.Services;

namespace CloudResourceOptimizer.API.Orchestration;

public class AgenticAIOrchestrator
{
    private readonly AzureResourceService _resourceService;
    private readonly CostAnalysisService _costService;
    private readonly ActionExecutor _executor;

    public AgenticAIOrchestrator(
        AzureResourceService resourceService,
        CostAnalysisService costService,
        ActionExecutor executor)
    {
        _resourceService = resourceService;
        _costService = costService;
        _executor = executor;
    }

    public async Task<string> OptimizeResourcesAsync()
    {
        // Example flow:
        var resources = await _resourceService.GetResourcesAsync();
        var costInsights = await _costService.AnalyzeCostsAsync(resources);

        var actions = _executor.SuggestOptimizations(resources, costInsights);

        // (Optional) Execute actions or send notifications
        await _executor.ExecuteActionsAsync(actions);

        return "Optimization analysis complete and actions executed.";
    }
}
