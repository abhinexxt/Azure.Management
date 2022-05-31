using System;

namespace Azure.Management.Provision.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public void LogActivity(string message) => 
            Console.WriteLine(message);
        
    }
}
