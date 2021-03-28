using Events.API.Infra.Options;
using Events.API.Models.ViewModels;
using Events.API.Services.EventsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events.API.Services.HostedServices
{
    public class EventsConsumerHostedService : BackgroundService
    {
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public EventsConsumerHostedService(IOptions<RabbitMqOptions> rabbitMqOptions, IServiceProvider serviceProvider)
        {
            _rabbitMqOptions = rabbitMqOptions.Value;
            _serviceProvider = serviceProvider;

            InitializeRabbitMq();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                using var serviceScope = _serviceProvider.CreateScope();
                var eventServices = serviceScope.ServiceProvider.GetRequiredService<IEventServices>();

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (ch, ea) =>
                {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var eventViewModel = JsonConvert.DeserializeObject<EventViewModel>(content);

                    await eventServices.ProcessEvents(eventViewModel);
                };

                _channel.BasicConsume(queue: _rabbitMqOptions.QueueName, autoAck: true, consumer: consumer);
            }
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.HostName,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: _rabbitMqOptions.QueueName, durable: true, exclusive: false,
                autoDelete: false, arguments: null);
        }
    }
}
