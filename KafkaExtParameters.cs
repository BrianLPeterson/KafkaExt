using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Newtonsoft.Json.Linq;

namespace CustomLAExtension
{
    internal class KafkaExtParameters
    {
        private InsensitiveDictionary<JToken> connectionParameters;
        private ServiceOperationRequest serviceOperationRequest;

        public string Content { get; }

        public KafkaExtParameters(InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            this.connectionParameters = connectionParameters;
            this.serviceOperationRequest = serviceOperationRequest;
            
        }
    }
}

