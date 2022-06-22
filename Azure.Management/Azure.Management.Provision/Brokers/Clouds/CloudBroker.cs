using System;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Azure.Management.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string tenantId;
        private readonly string adminName;
        private readonly string adminAccess;
        private readonly IAzure azure;

        public CloudBroker()
        {
            //this.clientId = Environment.GetEnvironmentVariable("AzureClientId");
            //this.clientSecret = Environment.GetEnvironmentVariable("AzureClientSecret");
            //this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            //this.adminName = Environment.GetEnvironmentVariable("AzureAdminName");
            //this.adminAccess = Environment.GetEnvironmentVariable("AzureAdminAccess");

            this.clientId = "ab2adbe5-1e2d-47e7-a57f-813745f75a75";
            this.clientSecret = "S.T8Q~5w3~J4RiK.Cw59r3IMPVbCTYi6sIYjjbrR";
            this.tenantId = "4b03170f-43a5-4348-b086-afcd50dc28c7";
            this.adminName = "AzureAdminName";
            this.adminAccess = "AzureAdminAccess";
            this.azure = AuthenticateAzure();
        }

        private IAzure AuthenticateAzure()
        {
            AzureCredentials credentials =
                SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                    clientId: this.clientId,
                    clientSecret: this.clientSecret,
                    tenantId: this.tenantId,
                    environment: AzureEnvironment.AzureGlobalCloud);

            return Microsoft.Azure.Management.Fluent.Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription("7a72c383-dade-4f73-898b-376916d83d02");
        }
    }
}
