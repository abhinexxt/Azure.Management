using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Management.Provision.Brokers.Configurations;
using Azure.Management.Provision.Models.Configurations;
using Azure.Management.Provision.Models.Storages;
using Azure.Management.Provision.Services.Foundations.CloudManagements;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace Azure.Management.Provision.Services.Processings.CloudManagements
{
    public class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly ICloudManagementService cloudManagementService;
        private readonly IConfigurationsBroker configurationsBroker;

        public CloudManagementProcessingService()
        {
            this.cloudManagementService = new CloudManagementService();
            this.configurationsBroker = new ConfigurationsBroker();
        }
        public async ValueTask ProcessAsync()
        {
            CloudManagementConfiguration cloudManagementConfiguration = 
                this.configurationsBroker.GetConfigurations();
            
            await ProvisionAsync(
                projectName: cloudManagementConfiguration.ProjectName,
                cloudAction: cloudManagementConfiguration.Up);
        }

        private async ValueTask ProvisionAsync(
            string projectName,
            CloudAction cloudAction)
        {
            List<string> environments = RetrieveEnvironments(cloudAction);

            foreach (string environmentName in environments)
            {
                IResourceGroup resourceGroup =
                    await this.cloudManagementService.ProvisionResourceGroupAsync(projectName, environmentName);

                IAppServicePlan appServicePlan =
                    await this.cloudManagementService
                        .ProvisionAppServicePlanAsync(projectName, environmentName, resourceGroup);

                ISqlServer sqlServer =
                    await this.cloudManagementService
                        .ProvisionSqlServerAsync(projectName,environmentName,resourceGroup);

                SqlDatabase sqlDatabase =
                    await this.cloudManagementService
                        .ProvisionSqlDatabaseAsync(projectName, environmentName, sqlServer);

                IWebApp webApp = await this.cloudManagementService
                        .ProvisionWebAppAsync(
                            projectName,
                            environmentName,
                            sqlDatabase.ConnectionString,
                            appServicePlan,
                            resourceGroup);
            }
        }

        private static List<string> RetrieveEnvironments(CloudAction cloudAction) =>
            cloudAction.Environments ?? new List<string>();
        
    }
}
