using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    public static class GetIncidentApiOperationManifest
    {
        internal const string OperationId = "ConsumeMessage";

        internal static readonly ServiceOperationManifest OperationManifest; 

        static GetIncidentApiOperationManifest()
        {
            OperationManifest = new ServiceOperationManifest()
            {
                ConnectionReference = new ConnectionReferenceFormat
                {
                    ReferenceKeyFormat = ConnectionReferenceKeyFormat.ServiceProvider,
                },
                Settings = new OperationManifestSettings()
                {
                    SecureData = new OperationManifestSettingWithOptions<SecureDataOptions>(),
                    TrackedProperties = new OperationManifestSetting()
                    {
                        Scopes = new OperationScope[] { OperationScope.Action }
                    },
                    RetryPolicy = new OperationManifestSetting()
                    {
                        Scopes = new OperationScope[] { OperationScope.Action }
                    }
                },
                InputsLocation = new InputsLocation[]
                {
                    InputsLocation.Inputs,
                    InputsLocation.Parameters,
                },
                Inputs = new SwaggerSchema
                {
                    Type = SwaggerSchemaType.Object,
                    Properties = new OrdinalDictionary<SwaggerSchema>
                    {
                        {
                            "Id", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "Id",
                                Description = "Incident Id",
                            }
                        },
                    },
                    Required = new string[]
                    {
                        "Id" ,
                    },
                },
                Outputs = new SwaggerSchema
                {
                    Type = SwaggerSchemaType.Object,
                    Properties = new OrdinalDictionary<SwaggerSchema>
                    {
                        {
                            "hmm", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "hmm",
                                Description = "hmmm",
                            }
                        },
                    },
                },
                Connector = IcMApiOperationsDataProvider.GetServiceOperationApi()
            };
        }


        internal static readonly ServiceOperation Operation = new ServiceOperation()
        {
            Name = "GetIncident",
            Id = "GetIncident",
            Type = "GetIncident",
            Properties = new ServiceOperationProperties()
            {
                Api = IcMApiOperationsDataProvider.GetServiceOperationApi().GetFlattenedApi(),
                Summary = "Get Incident",
                Description = "Get Incident",
                Visibility = Visibility.Important,
                OperationType = OperationType.ServiceProvider,
                BrandColor = IcMApiOperationsDataProvider.GetServiceOperationApi().Properties.BrandColor,
                IconUri = IcMApiOperationsDataProvider.GetServiceOperationApi().Properties.IconUri,
                Annotation = new ServiceOperationAnnotation()
                { 
                    Status = StatusAnnotation.Preview,
                    Family = "/serviceProviders/IcM"
                }
            }
        };

    }
}
