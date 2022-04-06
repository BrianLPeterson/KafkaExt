using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    [Extension("IcMServiceProvider", configurationSection: "IcMServiceProvider")]
    public class IcMServiceProvider : IExtensionConfigProvider
    {
        public IcMServiceProvider(
            ServiceOperationsProvider serviceOperationsProvider,
            IcMServiceOperationProvider operationsProvider)
        {
            serviceOperationsProvider.RegisterService(serviceName: IcMServiceOperationProvider.ServiceName, serviceOperationsProviderId: IcMServiceOperationProvider.ServiceId, serviceOperationsProviderInstance: operationsProvider);
        }

        public void Initialize(ExtensionConfigContext context)
        {
            
        }
    }
}
