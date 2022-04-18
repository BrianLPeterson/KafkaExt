using Azure.Identity;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.Azure.Workflows.ServiceProviders.WebJobs.Abstractions.Providers;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CustomLAExtension.Providers
{
    [ServiceOperationsProvider(Id = IcMServiceOperationProvider.ServiceId, Name = IcMServiceOperationProvider.ServiceName)]
    public class IcMServiceOperationProvider : IServiceOperationsProvider
    {
        public const string ServiceName = "IcM";

        public const string ServiceId = "/serviceProviders/IcM";

        private readonly List<ServiceOperation> serviceOperationsList;

        private readonly InsensitiveDictionary<ServiceOperation> apiOperationsList;


        public IcMServiceOperationProvider()
        {
            serviceOperationsList = new List<ServiceOperation>();
            apiOperationsList = new InsensitiveDictionary<ServiceOperation>();

            this.apiOperationsList.AddRange(new InsensitiveDictionary<ServiceOperation>
            {
                { "GetIncident", GetIncidentApiOperationManifest.Operation },
            });

            this.serviceOperationsList.AddRange(new List<ServiceOperation>
            {
                {
                    GetIncidentApiOperationManifest.Operation.CloneWithManifest(GetIncidentApiOperationManifest.OperationManifest)
                }
            });
        }
        

        string IServiceOperationsProvider.GetBindingConnectionInformation(string operationId, InsensitiveDictionary<JToken> connectionParameters)
        {
            return ServiceOperationsProviderUtilities
                    .GetRequiredParameterValue(
                        serviceId: ServiceId,
                        operationId: operationId,
                        parameterName: "secretId",
                        parameters: connectionParameters)?
                    .ToValue<string>();
        }

        IEnumerable<ServiceOperation> IServiceOperationsProvider.GetOperations(bool expandManifest)
        {
            return expandManifest ? serviceOperationsList : GetApiOperations();
        }

        ServiceOperationApi IServiceOperationsProvider.GetService()
        {
            return IcMApiOperationsDataProvider.GetServiceOperationApi();
        }

        Task<ServiceOperationResponse> IServiceOperationsProvider.InvokeOperation(string operationId, InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            var secretId = ServiceOperationsProviderUtilities.GetRequiredParameterValue(ServiceName, operationId, "secretId", connectionParameters).ToValue<string>();

            if (operationId == "GetIncident")
            {
                Azure.Security.KeyVault.Certificates.CertificateClient client = new Azure.Security.KeyVault.Certificates.CertificateClient(new Uri("https://workflowsecrets-test.vault.azure.net"), new DefaultAzureCredential());

                var cert = client.GetCertificate("secretId");

                return Task.FromResult((ServiceOperationResponse)new IcMResponse(JObject.FromObject(new { Status = $"{secretId} success {cert.Value.Name}" }), System.Net.HttpStatusCode.OK));
            }
            else
            {
                return Task.FromResult((ServiceOperationResponse)new IcMResponse(JObject.FromObject(new { Status = $"{operationId} not found" }), System.Net.HttpStatusCode.OK));
            }
        } 

        private IEnumerable<ServiceOperation> GetApiOperations()
        {
            return this.apiOperationsList.Values;
        } 
    }
}
