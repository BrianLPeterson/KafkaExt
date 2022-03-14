using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    [Extension("KafkaExtServiceProvider", configurationSection: "KafkaExtServiceProvider")]
    public class KafkaExtServiceProvider : IExtensionConfigProvider
    {
        public KafkaExtServiceProvider(
            ServiceOperationsProvider serviceOperationsProvider,
            KafkaExtServiceOperationProvider operationsProvider)
        {
            serviceOperationsProvider.RegisterService(serviceName: KafkaExtServiceOperationProvider.ServiceName, serviceOperationsProviderId: KafkaExtServiceOperationProvider.ServiceId, serviceOperationsProviderInstance: operationsProvider);
        }

        public void Initialize(ExtensionConfigContext context)
        {
            
        }
    }
}
