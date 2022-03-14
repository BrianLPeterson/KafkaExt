using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    public static class WriteMessageApiOperationManifest
    {
        internal const string OperationId = "WriteMessage";

        internal static readonly ServiceOperationManifest OperationManifest; 

        static WriteMessageApiOperationManifest()
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
                            "content", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "Content",
                                Description = "Content",
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
                        "content" ,
                        "topic"
                    },
                },
                Outputs = new SwaggerSchema
                {
                    Type = SwaggerSchemaType.Object,
                    Properties = new OrdinalDictionary<SwaggerSchema>
                    {
                        {
                            "message", new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.String,
                                Title = "message",
                                Description = "message",
                            }
                        },
                    },
                },
                Connector = KafkaExtApiOperationsDataProvider.GetServiceOperationApi()
            };
        }


        internal static readonly ServiceOperation Operation = new ServiceOperation()
        {
            Name = "WriteMessage",
            Id = "WriteMessage",
            Type = "WriteMessage",
            Properties = new ServiceOperationProperties()
            {
                Api = KafkaExtApiOperationsDataProvider.GetServiceOperationApi().GetFlattenedApi(),
                Summary = "Write a Message to a Kafka Topic",
                Description = "Write a Message to a Kafka Topic",
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
