using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;

namespace CustomLAExtension.Providers
{
    /// <summary>
    /// IcMConnectionParameters API connection parameters
    /// </summary>
    public class IcMConnectionParameters : ConnectionParameters
    {
        public ConnectionStringParameters SecretId { get; set; }
    }
}
