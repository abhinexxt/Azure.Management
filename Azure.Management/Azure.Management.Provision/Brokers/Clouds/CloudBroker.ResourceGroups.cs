using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(resourceGroupName);

        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName)
        {
            return await this.azure.ResourceGroups
                .Define(resourceGroupName)
                .WithRegion(Region.EuropeWest)
                .CreateAsync();
        }

        public async ValueTask DeleteResourceGroupAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.DeleteByNameAsync(resourceGroupName);



    }
}
