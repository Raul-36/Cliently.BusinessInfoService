using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Application.Common.Messaging.Consumers.Base;
using Infrastructure.Messaging.Options;
using Microsoft.Extensions.Options;
using Infrastructure.Users.Options;
using System.Text.Json;
using Application.Users.Events;
using MediatR;
using AutoMapper;
using Application.Users.Commands;
using System.Net.Cache;
using Application.Users.DTOs.Requests;

namespace Infrastructure.Users.Consumers
{
    public class UserCreatedConsumer : IConsumer, IDisposable
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly string queueName;
        private readonly ConnectionFactory factory;
        private IConnection? connection;
        private IChannel? channel;

        public UserCreatedConsumer(IMapper mapper, IMediator mediator,IOptions<RabbitMQOptions> rmqOptions, IOptions<UserQueuesOptions> uqOptions)
        {
            this.mediator = mediator;
            this.mapper = mapper;


            queueName = uqOptions.Value.CreateUser;

            factory = new ConnectionFactory
            {
                HostName = rmqOptions.Value.HostName,
                UserName = rmqOptions.Value.UserName,
                Password = rmqOptions.Value.Password
            };
        }

        private async Task EnsureConnectedAsync()
        {
            if (connection != null && channel != null && channel.IsOpen)
                return;

            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
        }

        public async Task ExecuteAsync()
        {
            await EnsureConnectedAsync();

            if (channel == null)
                throw new InvalidOperationException("RabbitMQ channel is not initialized.");

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, deliverEventArgs) =>
            {
                try
                {
                    var content = Encoding.UTF8.GetString(deliverEventArgs.Body.ToArray());
                    var createdUser = JsonSerializer.Deserialize<UserCreatedEvent>(content);

                    Console.WriteLine($"Received: Create user ({content})");
                    
                    var request = mapper.Map<CreateUserRequest>(createdUser);
                    var command = new CreateUserCommand{ Request = request };
                    var response = await mediator.Send(command);
                    Console.WriteLine($"Creted: user({response.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("Consumer started. Listening for messages...");
        }

        public void Dispose()
        {
            channel?.Dispose();
            connection?.Dispose();
        }
    }
}
