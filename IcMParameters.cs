
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Newtonsoft.Json.Linq;

namespace CustomLAExtension
{
    internal class IcMParameters
    {
        private InsensitiveDictionary<JToken> connectionParameters;
        private ServiceOperationRequest serviceOperationRequest;

        public string Content { get; }

        public IcMParameters(InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            this.connectionParameters = connectionParameters;
            this.serviceOperationRequest = serviceOperationRequest;
        }
    }
}

