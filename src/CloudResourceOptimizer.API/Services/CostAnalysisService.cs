using System;

namespace CloudResourceOptimizer.API.Services;

public class CostAnalysisService
{
    public async Task<Dictionary<string, decimal>> AnalyzeCostsAsync(List<string> resources)
    {
        // TODO: Call Azure Cost Management API
        await Task.Delay(100); // Simulate async work
        return resources.ToDictionary(r => r, r => 100.00m);
    }
}
