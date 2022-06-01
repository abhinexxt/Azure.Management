using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Azure.Management.Provision.Services.Foundations.CloudManagement
{
    public interface ICloudManagementService
    {
        ValueTask<IResourceGroup> ProvisionResourceGroupAsync(string projectName, string environment);

        ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName, 
            string environment, 
            IResourceGroup resourceGroup);
    }
}
