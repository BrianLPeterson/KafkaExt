using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    public static class ConsumeMessageApiOperationManifest
    {
        internal const string OperationId = "ConsumeMessage";

        internal static readonly ServiceOperationManifest OperationManifest; 

        static ConsumeMessageApiOperationManifest()
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
                            "timeout", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "Timeout",
                                Description = "Timeout",
                            }
                        },
                        {
                            "topic", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "Topic",
                                Description = "Topic",
                            }
                        }
                    },
                    Required = new string[]
                    {
                        "Timeout" ,
                        "topic"
                    },
                },
                Outputs = new SwaggerSchema
                {
                    Type = SwaggerSchemaType.Object,
                    Properties = new OrdinalDictionary<SwaggerSchema>
                    {
                        {
                            "body", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "body",
                                Description = "body",
                            }
                        },
                    },
                },
                Connector = KafkaExtApiOperationsDataProvider.GetServiceOperationApi()
            };
        }


        internal static readonly ServiceOperation Operation = new ServiceOperation()
        {
            Name = "ConsumeMessage",
            Id = "ConsumeMessage",
            Type = "ConsumeMessage",
            Properties = new ServiceOperationProperties()
            {
                Api = KafkaExtApiOperationsDataProvider.GetServiceOperationApi().GetFlattenedApi(),
                Summary = "Consume a Message to a Kafka Topic",
                Description = "Consume a Message to a Kafka Topic",
                Visibility = Visibility.Important,
                OperationType = OperationType.ServiceProvider,
                BrandColor = KafkaExtApiOperationsDataProvider.GetServiceOperationApi().Properties.BrandColor,
                IconUri = KafkaExtApiOperationsDataProvider.GetServiceOperationApi().Properties.IconUri,
                Annotation = new ServiceOperationAnnotation()
                { 
                    Status = StatusAnnotation.Preview,
                    Family = "/serviceProviders/KafkaExt"
                }
            }
        };

    }
}
