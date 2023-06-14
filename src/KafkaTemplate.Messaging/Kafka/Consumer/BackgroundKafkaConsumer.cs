using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public class BackgroundKafkaConsumer : BackgroundService
    {
        private readonly IKafkaMessageConsumerFactory _kafkaMessageConsumerFactory;
        private readonly ILogger<BackgroundKafkaConsumer> _logger;

        public BackgroundKafkaConsumer(IKafkaMessageConsumerFactory kafkaMessageConsumerFactory, ILogger<BackgroundKafkaConsumer> logger)
        {
            _kafkaMessageConsumerFactory = kafkaMessageConsumerFactory;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try 
            {
                return Task.Run(() => _kafkaMessageConsumerFactory.StartConsmers(stoppingToken), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BackgroundKafkaConsumer error. {ex.Message}");
                throw;
            }
        }
    }
}
