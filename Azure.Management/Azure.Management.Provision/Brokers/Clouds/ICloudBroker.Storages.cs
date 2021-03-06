using System.Threading.Tasks;
using Azure.Management.Provision.Models.Storages;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<ISqlServer> CreateSqlServerAsync(string sqlServerName, IResourceGroup resourceGroup);

        ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(string sqlDatabaseName, ISqlServer sqlServer);

        SqlDatabaseAccess GetSqlDatabaseAccess();
    }
}
