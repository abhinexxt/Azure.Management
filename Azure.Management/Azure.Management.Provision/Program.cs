using System;
using System.Threading.Tasks;
using Azure.Management.Provision.Services.Processings.CloudManagements;

namespace Azure.Management.Provision
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cloudManagementProcessingService =
                new CloudManagementProcessingService();

            await cloudManagementProcessingService.ProcessAsync();

            Console.ReadKey();
        }
    }
}
