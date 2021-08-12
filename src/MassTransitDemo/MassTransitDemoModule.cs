using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MassTransitDemo
{

    [DependsOn(
        typeof(AbpAutofacModule)
    )]
    public class MassTransitDemoModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<MassTransitDemoHostedService>();


            context.Services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("192.168.0.50", 5672, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<PublishMessage>(x =>
                    {
                        x.SetEntityName("my.publish3");
                    });

                    cfg.Publish<PublishMessage>(x =>
                    {
                        x.AutoDelete = false;
                        x.Durable = true;
                        x.ExchangeType = "fanout";
                    });

                    cfg.ReceiveEndpoint("my.publish3-2", e =>
                    {
                        e.AutoDelete = false;
                        e.Durable = true;
                        e.ExchangeType = "fanout";
                        e.Consumer<MessageConsumer>(context);
                    });

                });

            }).AddMassTransitHostedService(true);

        }
    }
}
