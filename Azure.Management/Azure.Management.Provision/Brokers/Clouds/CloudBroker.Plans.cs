﻿using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IAppServicePlan> CreatePlanAsync(string planName, IResourceGroup resourceGroup)
        {
            return await this.azure.AppServices.AppServicePlans
                .Define(planName)
                .WithRegion(Region.EuropeWest)
                .WithExistingResourceGroup(resourceGroup)
                .WithPricingTier(PricingTier.StandardS1)
                .WithOperatingSystem(Microsoft.Azure.Management.AppService.Fluent.OperatingSystem.Windows)
                .CreateAsync();
        }
    }
}