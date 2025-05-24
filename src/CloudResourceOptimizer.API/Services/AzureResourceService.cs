using System;

namespace CloudResourceOptimizer.API.Services;

public class AzureResourceService
{
    public async Task<List<string>> GetResourcesAsync()
    {
        // TODO: Call Azure Resource Graph API or Resource Management SDK
        await Task.Delay(100); // Simulate async work
        return new List<string> { "VM1", "DB1", "Storage1" };
    }
}
