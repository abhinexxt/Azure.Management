using System.IO;
using Azure.Management.Provision.Models.Configurations;
using Microsoft.Extensions.Configuration;

namespace Azure.Management.Provision.Brokers.Configurations
{
    public class ConfigurationsBroker
    {
        public CloudManagementConfiguration GetConfiguration()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appSettings.json", optional: false)
                .Build();

            return configurationRoot.Get<CloudManagementConfiguration>();
        }
    }
}
