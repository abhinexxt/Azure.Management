using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Management.Provision.Models.Configurations
{
    public class CloudManagementConfiguration
    {
        public string ProjectName { get; set; }

        public CloudAction Up { get; set; }

        public CloudAction Down { get; set; }
    }
}
