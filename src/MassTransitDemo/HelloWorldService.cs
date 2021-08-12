using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MassTransitDemo
{
    public class HelloWorldService : ITransientDependency
    {
        protected IPublishEndpoint PublishEndpoint { get; }
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        public HelloWorldService(IServiceScopeFactory serviceScopeFactory, IPublishEndpoint publishEndpoint)
        {
            ServiceScopeFactory = serviceScopeFactory;
            PublishEndpoint = publishEndpoint;
        }


        public virtual async Task PublishAsync()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

            for (int i = 0; i < 10; i++)
            {
                await publishEndpoint.Publish(new PublishMessage()
                {
                    Id = i,
                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")
                });
            }

        }


        public void SayHello()
        {
            Console.WriteLine("\tHello World!");
        }
    }
}
