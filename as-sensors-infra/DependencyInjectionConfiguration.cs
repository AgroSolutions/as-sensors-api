using Amazon.SQS;
using as_sensors_domain.Messaging.Interfaces;
using as_sensors_infra.Messaging.Sqs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace as_sensors_infra
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureAmazonSQS(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSQS>();

            services.AddTransient<IQueuePublisher, AmazonSqsPublisher>();
            services.AddTransient<IQueueConsumer, AmazonSqsConsumer>();
            return services;
        }
    }
}
