using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MassTransitDemo
{
    public class MessageConsumer : IConsumer<PublishMessage>
    {
        private readonly ILogger _logger;
        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PublishMessage> context)
        {
            // _logger.LogInformation("Consume message id '{0}',timestamp :'{1}'.", context.Message.Id, context.Message.Timestamp);
            Console.WriteLine("Consume message id '{0}',timestamp :'{1}'.", context.Message.Id, context.Message.Timestamp);

            return Task.CompletedTask;
        }
    }
}
