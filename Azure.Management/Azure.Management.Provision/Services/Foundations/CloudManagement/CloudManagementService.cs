using System;
using System.Threading.Tasks;
using Azure.Management.Provision.Brokers.Clouds;
using Azure.Management.Provision.Brokers.Loggings;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Azure.Management.Provision.Services.Foundations.CloudManagement
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService()
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }
        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(string projectName, string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();

            this.loggingBroker.LogActivity(
                message: $"Provisioning {resourceGroupName} ...");

            IResourceGroup resourceGroup = 
                await this.cloudBroker.CreateResourceGroupAsync(resourceGroupName);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {resourceGroupName} completed!");

            return resourceGroup;
        }
    }
}
