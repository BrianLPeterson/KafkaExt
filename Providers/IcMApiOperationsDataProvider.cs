using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    public static class IcMApiOperationsDataProvider
    {
        public static ServiceOperationApi GetServiceOperationApi()
        {
            return new ServiceOperationApi()
            {
                Name = "IcM",
                Id = "/serviceProviders/IcM",
                Type = DesignerApiType.ServiceProvider,
                Properties = new ServiceOperationApiProperties
                {
                    BrandColor = 4287090426,
                    IconUri = new Uri(CustomLAExtension.Properties.Resources.IconUri),
                    Description = "IcM",
                    DisplayName = "IcM",
                    Capabilities = new ApiCapability[] { ApiCapability.Actions },
                    ConnectionParameters = new IcMConnectionParameters
                    {
                        SecretId = new ConnectionStringParameters
                        {
                            Type = ConnectionStringType.StringType,
                            ParameterSource = ConnectionParameterSource.NotSpecified,
                            UIDefinition = new UIDefinition
                            {
                                DisplayName = "Secret Id",                                
                                Description = "Secret Id",
                                Tooltip = "Secret Id",
                                Constraints = new Constraints
                                {
                                    Required = "true"
                                },
                            },
                        },
                    },
                },
            };
        }
    }
}
