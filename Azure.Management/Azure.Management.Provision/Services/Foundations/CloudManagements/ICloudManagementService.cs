using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Management.Provision.Models.Storages;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace Azure.Management.Provision.Services.Foundations.CloudManagements
{
    public interface ICloudManagementService
    {
        ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment);

        ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName, 
            string environment, 
            IResourceGroup resourceGroup);

        ValueTask<ISqlServer> ProvisionSqlServerAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup);

        ValueTask<SqlDatabase> ProvisionSqlDatabaseAsync(
            string projectName,
            string environment,
            ISqlServer sqlServer);

        ValueTask<IWebApp> ProvisionWebAppAsync(
            string applicationName,
            string projectName,
            string environment,
            string dbConnectionString,
            IAppServicePlan appServicePlan,
            IResourceGroup resourceGroup);

        ValueTask DeprovisionResourceGroupAsync(
            string projectName,
            string environment);
    }
}
