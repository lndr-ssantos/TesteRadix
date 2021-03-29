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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IModel _channel;

        public EventsConsumerHostedService(IOptions<RabbitMqOptions> rabbitMqOptions, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMqOptions = rabbitMqOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;

            InitializeRabbitMq();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            var eventServices = serviceScope.ServiceProvider.GetRequiredService<IEventServices>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (ch, ea) =>
                {
                    try
                    {
                        var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                        var eventViewModel = JsonConvert.DeserializeObject<EventViewModel>(content);

                        await eventServices.ProcessEvents(eventViewModel);
                    }
                    catch (Exception ex)
                    {
                        //Em um projeto de verdade eu teria uma solução real para log
                        Console.WriteLine($"Mensagem inválida recebida.\n\nExceção: {ex.Message}");
                    }
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
