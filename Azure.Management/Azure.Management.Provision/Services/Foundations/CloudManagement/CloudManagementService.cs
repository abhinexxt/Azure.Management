using System.Threading.Tasks;
using Azure.Management.Provision.Brokers.Clouds;
using Azure.Management.Provision.Brokers.Loggings;
using Azure.Management.Provision.Models.Storages;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

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

        public async ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string appServicePlanName = $"{projectName}-PLAN-{environment}".ToUpper();

            this.loggingBroker.LogActivity(
                message: $"Provisioning {appServicePlanName} ...");

            IAppServicePlan appServicePlan =
                await this.cloudBroker.CreatePlanAsync(appServicePlanName, resourceGroup);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {appServicePlanName} completed!");

            return appServicePlan;
        }

        public async ValueTask<ISqlServer> ProvisionSqlServerAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string sqlServerName = $"{projectName}-dbserver-{environment}".ToLower();

            this.loggingBroker.LogActivity(
                message: $"Provisioning {sqlServerName} ...");

            ISqlServer sqlServer = await this.cloudBroker.CreateSqlServerAsync(sqlServerName, resourceGroup);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {sqlServerName} completed!");

            return sqlServer;
        }

        public async ValueTask<SqlDatabase> ProvisionSqlDatabaseAsync(
            string projectName,
            string environment,
            ISqlServer sqlServer)
        {
            string sqlDatabaseName = $"{projectName}-db-{environment}".ToLower();

            this.loggingBroker.LogActivity(
                message: $"Provisioning {sqlDatabaseName} ...");

            ISqlDatabase sqlDatabase =
                await this.cloudBroker.CreateSqlDatabaseAsync(sqlDatabaseName, sqlServer);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {sqlDatabaseName} completed!");

            return new SqlDatabase
            {
                Database = sqlDatabase,
                ConnectionString = GenerateDbConnectionString(sqlDatabase)
            };
        }

        private string GenerateDbConnectionString(ISqlDatabase sqlDatabase)
        {
            SqlDatabaseAccess access = this.cloudBroker.GetSqlDatabaseAccess();
            return  $"Server=tcp:{sqlDatabase.SqlServerName}.database.windows.net,1433;" + 
                    $"Initial Catalog={sqlDatabase.Name};" +
                    $"User ID={access.AdminName};" +
                    $"Password={access.AdminAccess};";
        }
    }
}
