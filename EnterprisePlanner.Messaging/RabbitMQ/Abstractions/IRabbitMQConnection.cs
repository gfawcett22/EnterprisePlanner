using RabbitMQ.Client;
using System;

namespace EnterprisePlanner.Messaging.RabbitMQ.Abstractions
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
