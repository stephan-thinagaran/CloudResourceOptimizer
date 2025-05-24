using System;

namespace CloudResourceOptimizer.API.Services;

public class ActionExecutor
{
    public List<string> SuggestOptimizations(List<string> resources, Dictionary<string, decimal> costs)
    {
        // Example: Identify expensive resources
        return resources.Where(r => costs[r] > 80).ToList();
    }

    public async Task ExecuteActionsAsync(List<string> actions)
    {
        // TODO: Use Azure SDK to scale down or delete resources
        await Task.Delay(100);
        Console.WriteLine("Executed actions: " + string.Join(", ", actions));
    }
}
