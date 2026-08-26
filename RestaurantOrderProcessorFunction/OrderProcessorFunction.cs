using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using RestaurantOrderProcessorFunction.Models;

namespace RestaurantOrderProcessorFunction
{
    public class OrderProcessorFunction
    {
        private readonly ILogger<OrderProcessorFunction> _logger;
        private static readonly HashSet<int> _processedOrderIds = new();

        public OrderProcessorFunction(ILogger<OrderProcessorFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(OrderProcessorFunction))]
        public async Task Run(
            [ServiceBusTrigger("orders-queue", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var orderMessage = JsonSerializer.Deserialize<OrderCreatedMessage>(message.Body.ToString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (orderMessage == null)
            {
                throw new Exception("Invalid OrderCreated message.");
            }

            if (_processedOrderIds.Contains(orderMessage.OrderId))
            {
                _logger.LogInformation(
                    "Order {orderId} already processed. Skipping duplicate message.",
                    orderMessage.OrderId);

                await messageActions.CompleteMessageAsync(message);
                return;
            }

            _logger.LogInformation(
    "Processing Order {orderId} for Table {tableNumber}, Status {status}",
    orderMessage.OrderId,
    orderMessage.TableNumber,
    orderMessage.Status);

            _processedOrderIds.Add(orderMessage.OrderId);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
