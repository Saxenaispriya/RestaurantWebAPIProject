using Azure.Messaging.ServiceBus;

namespace RestaurantWebAPIProject.Messaging
{
    public class ServiceBusMessageSender
    {
        private readonly ServiceBusSender _sender;

        public ServiceBusMessageSender(IConfiguration configuration)
        {
            var connectionString = configuration["ServiceBus:ConnectionString"];
            var queueName = configuration["ServiceBus:QueueName"];

            var client = new ServiceBusClient(connectionString);
            _sender=client.CreateSender(queueName);
        }

        public async Task SendMessageAsync(string messageBody)
        {
            var message=new ServiceBusMessage(messageBody);

            await _sender.SendMessageAsync(message);
        }
    }
}
