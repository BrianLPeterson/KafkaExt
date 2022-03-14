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
    [ServiceOperationsProvider(Id = KafkaExtServiceOperationProvider.ServiceId, Name = KafkaExtServiceOperationProvider.ServiceName)]
    public class KafkaExtServiceOperationProvider : IServiceOperationsProvider
    {
        public const string ServiceName = "KafkaExt";

        public const string ServiceId = "/serviceProviders/KafkaExt";

        private readonly List<ServiceOperation> serviceOperationsList;

        private readonly InsensitiveDictionary<ServiceOperation> apiOperationsList;


        public KafkaExtServiceOperationProvider()
        {
            serviceOperationsList = new List<ServiceOperation>();
            apiOperationsList = new InsensitiveDictionary<ServiceOperation>();

            this.apiOperationsList.AddRange(new InsensitiveDictionary<ServiceOperation>
            {
                { "HelloWorld", WriteMessageApiOperationManifest.Operation },
            });

            this.serviceOperationsList.AddRange(new List<ServiceOperation>
            {
                {  WriteMessageApiOperationManifest.Operation.CloneWithManifest(WriteMessageApiOperationManifest.OperationManifest) }
            });
        }
        

        string IServiceOperationsProvider.GetBindingConnectionInformation(string operationId, InsensitiveDictionary<JToken> connectionParameters)
        {
            return ServiceOperationsProviderUtilities
                    .GetRequiredParameterValue(
                        serviceId: ServiceId,
                        operationId: operationId,
                        parameterName: "connectionString",
                        parameters: connectionParameters)?
                    .ToValue<string>();
        }

        IEnumerable<ServiceOperation> IServiceOperationsProvider.GetOperations(bool expandManifest)
        {
            return expandManifest ? serviceOperationsList : GetApiOperations();
        }

        ServiceOperationApi IServiceOperationsProvider.GetService()
        {
            return KafkaExtApiOperationsDataProvider.GetServiceOperationApi();
        }

        Task<ServiceOperationResponse> IServiceOperationsProvider.InvokeOperation(string operationId, InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            
            //Read the connectionString from the app.settings file and split key pair values
            var connString = ServiceOperationsProviderUtilities.GetParameterValue("ConnectionString", connectionParameters).ToValue<string>()
                .Split(";")
                .Select(s=> s.Split('='))
                .ToDictionary(s=>s.First(), s=>s.Last());

            //create a producer config
            var config = new ProducerConfig
            {
                BootstrapServers = connString["BootstrapServers"],
                ClientId = Dns.GetHostName(),
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = connString["SaslUsername"],
                SaslPassword = connString["SaslPassword"]
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var t = producer.ProduceAsync(serviceOperationRequest.Parameters["topic"].ToString() , new Message<Null, string>() { Value= serviceOperationRequest.Parameters["content"].ToString()} );               
                producer.Flush();
            }
            return Task.FromResult((ServiceOperationResponse)new KafkaExtResponse(JObject.FromObject(new { Status = "Successfully posted" }), System.Net.HttpStatusCode.Created));

        } 

        private IEnumerable<ServiceOperation> GetApiOperations()
        {
            return this.apiOperationsList.Values;
        }

    }
}
