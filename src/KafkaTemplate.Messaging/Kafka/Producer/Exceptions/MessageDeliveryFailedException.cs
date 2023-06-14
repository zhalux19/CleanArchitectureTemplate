using KafkaTemplate.Core.Exceptions.AbstractExceptions;

namespace KafkaTemplate.Messaging.Kafka.Producer.Exceptions
{
    public class MessageDeliveryFailedException : DefaultException
    {
        public MessageDeliveryFailedException() : base("Kafka message delivery failed.")
        {
        }
    }
}
