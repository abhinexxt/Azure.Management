namespace Azure.Management.Provision.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}
