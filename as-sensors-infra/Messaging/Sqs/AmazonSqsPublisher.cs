using Amazon.SQS.Model;
using Amazon.SQS;
using as_sensors_domain.Messaging.Interfaces;
using Newtonsoft.Json;

namespace as_sensors_infra.Messaging.Sqs
{
    public class AmazonSqsPublisher(IAmazonSQS sqs) : IQueuePublisher
    {
        public async Task PublishAsync<T>(T message, string queueName, string? exchange = null, CancellationToken cancellationToken = default)
        {
            var queueUrlResponse = await sqs.GetQueueUrlAsync(queueName, cancellationToken);

            var request = new SendMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MessageBody = JsonConvert.SerializeObject(message)
            };

            await sqs.SendMessageAsync(request, cancellationToken);
        }
    }
}
