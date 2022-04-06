using System;
using System.Collections.Generic;
using System.Text;

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(CustomLAExtension.Startup.IcMStartup))]
namespace CustomLAExtension.Startup
{
    using CustomLAExtension.Providers;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class IcMStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<IcMServiceProvider>();
            builder.Services.TryAddSingleton<IcMServiceOperationProvider>();            
        }
    }
}

