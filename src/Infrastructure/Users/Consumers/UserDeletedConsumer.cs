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
using Application.Users.Commands;

namespace Infrastructure.Users.Consumers
{
    public class UserDeletedConsumer : IConsumer, IDisposable
    {
        private readonly string queueName;
        private readonly ConnectionFactory factory;
        private IConnection? connection;
        private IChannel? channel;
        private readonly IMediator mediator;

        public UserDeletedConsumer(IOptions<RabbitMQOptions> rmqOptions, IOptions<UserQueuesOptions> uqOptions, IMediator mediator)
        {
            queueName = uqOptions.Value.DeleteUser;
            this.mediator = mediator;

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
                    var deletedUser = JsonSerializer.Deserialize<UserDeletedEvent>(content);
                    
                    Console.WriteLine($"Received: Delete user ({content})");

                    await mediator.Send(new DeleteUserCommand { Id = deletedUser.Id });
                    Console.WriteLine($"Deleted: user ({deletedUser.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("UserDeletedConsumer started. Listening for messages...");
        }

        public void Dispose()
        {
            channel?.Dispose();
            connection?.Dispose();
        }
    }
}
