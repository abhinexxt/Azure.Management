using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName);
        ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName);
        ValueTask DeleteResourceGroupAsync(string resourceGroupName);

    }
}
