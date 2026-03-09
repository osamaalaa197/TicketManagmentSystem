using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Infrastructure;

namespace TicketManagementSystem.Infrastructure.Payment
{
    public class PaymentStrategyFactory:IPaymentStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IPaymentStrategy Create(string paymentMethod)
        {
            return paymentMethod switch
            {
                "PayPal" => _serviceProvider.GetRequiredService<PayPalPaymentStrategy>(),
                "Stripe" => _serviceProvider.GetRequiredService<StripePaymentStrategy>(),
                _ => throw new ArgumentException("Invalid payment method.")
            };
        }
    }
}
