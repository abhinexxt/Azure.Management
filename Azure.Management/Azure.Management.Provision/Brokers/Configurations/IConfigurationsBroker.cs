using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Management.Provision.Models.Configurations;

namespace Azure.Management.Provision.Brokers.Configurations
{
    public interface IConfigurationsBroker
    {
        CloudManagementConfiguration GetConfiguration();
    }
}
