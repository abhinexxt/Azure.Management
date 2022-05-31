using System.Threading.Tasks;
using Azure.Management.Provision.Models.Storages;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<ISqlServer> CreateSqlServerAsync(string sqlServerName, IResourceGroup resourceGroup)
        {
            return await this.azure.SqlServers
                .Define(sqlServerName)
                .WithRegion(Region.EuropeWest)
                .WithExistingResourceGroup(resourceGroup)
                .WithAdministratorLogin(this.adminName)
                .WithAdministratorPassword(this.adminAccess)
                .CreateAsync();

        }

        public async ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(string sqlDatabaseName, ISqlServer sqlServer)
        {
            return await this.azure.SqlServers.Databases
                .Define(sqlDatabaseName)
                .WithExistingSqlServer(sqlServer)
                .CreateAsync();
        }

        public SqlDatabaseAccess GetSqlDatabaseAccess()
        {
            return new SqlDatabaseAccess
            {
                AdminName = this.adminName,
                AdminAccess = this.adminAccess
            };
        }
    }
}
