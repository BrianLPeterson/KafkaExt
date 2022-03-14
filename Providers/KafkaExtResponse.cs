using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomLAExtension.Providers
{
    public class KafkaExtResponse : ServiceOperationResponse
    {
        public KafkaExtResponse(JToken body, HttpStatusCode statusCode)
            : base(body, statusCode)
        {
        }

        /// <summary>
        /// Completes the operation.
        /// </summary>
        public override Task CompleteOperation()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Fails the operation.
        /// </summary>
        public override Task FailOperation()
        {
            return Task.FromResult<object>(null);
        }
    }
}
