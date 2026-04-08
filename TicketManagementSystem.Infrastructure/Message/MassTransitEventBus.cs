using MassTransit;
using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;

namespace TicketManagementSystem.Infrastructure.Messaging
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint=publishEndpoint;
        }
        public Task PublishAsync<T>(T message)
        {
            return _publishEndpoint.Publish(message);
        }
    }
}
