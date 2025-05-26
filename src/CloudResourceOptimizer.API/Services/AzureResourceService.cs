using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;

namespace CloudResourceOptimizer.API.Services;

public class AzureResourceService
{
    private readonly ILogger<AzureResourceService> _logger;
    private readonly IConfiguration _configuration;
    private readonly ArmClient _armClient;

    public AzureResourceService(ILogger<AzureResourceService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCredential = false
        });

        _armClient = new ArmClient(credential);

        // Create the ARM client
        _armClient = new ArmClient(credential);
    }

    public async Task<List<string>> GetResourcesAsync()
    {
        _logger.LogInformation("Fetching Azure resources");

        try
        {
            var resources = new List<string>();

            // Get subscription from configuration
            var subscriptionId = _configuration["Azure:SubscriptionId"];
            _logger.LogInformation("Using subscription: {SubscriptionId}", subscriptionId);

            // Get the subscription resource object
            var subscription = await _armClient.GetSubscriptions()
                .GetAsync(subscriptionId);

            // Get all resources in the subscription
            var resourceGroupCollection = subscription.Value.GetResourceGroups();
            await foreach (var resourceGroup in resourceGroupCollection.GetAllAsync())
            {
                _logger.LogInformation("Processing resource group: {ResourceGroupName}", resourceGroup.Data.Name);

                // Get all resources in the resource group
                var resourceCollection = resourceGroup.GetGenericResources();
                foreach (var resource in resourceCollection)
                {
                    var resourceId = resource.Data.Id.ToString();
                    var resourceName = resource.Data.Name;
                    var resourceType = resource.Data.ResourceType.ToString();

                    _logger.LogDebug("Found resource: {ResourceName} ({ResourceType})", resourceName, resourceType);

                    // Add to our list with type and name for better identification
                    resources.Add($"{resourceType}/{resourceName}");
                }
            }

            _logger.LogInformation("Successfully retrieved {ResourceCount} Azure resources", resources.Count);
            return resources;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch Azure resources");
            throw;
        }
    }
    
    // Optional: Add a method to get resources with more details
    public async Task<Dictionary<string, GenericResourceData>> GetDetailedResourcesAsync()
    {
        _logger.LogInformation("Fetching detailed Azure resources");
        
        try
        {
            var resources = new Dictionary<string, GenericResourceData>();
            var subscriptionId = _configuration["Azure:SubscriptionId"];
            
            var subscription = await _armClient.GetSubscriptions()
                .GetAsync(subscriptionId);
            
            var resourceGroupCollection = subscription.Value.GetResourceGroups();
            await foreach (var resourceGroup in resourceGroupCollection.GetAllAsync())
            {
                var resourceCollection = resourceGroup.GetGenericResources();
                foreach (var resource in resourceCollection)
                {
                    var resourceId = resource.Data.Id.ToString();
                    resources.Add(resourceId, resource.Data);
                }
            }
            
            _logger.LogInformation("Successfully retrieved {ResourceCount} detailed Azure resources", resources.Count);
            return resources;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch detailed Azure resources");
            throw;
        }
    }
}


// Optional: Define a simplified resource model if needed
public class AzureResource
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Location { get; set; }
    public string? ResourceGroup { get; set; }
    public IDictionary<string, string>? Tags { get; set; }
}