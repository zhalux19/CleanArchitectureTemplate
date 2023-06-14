using AutoMapper;
using KafkaTemplate.Core.Commands.Accounts.CreateAccountConsumer;
using KafkaTemplate.Core.Entities;
using KafkaTemplate.Messaging.Contracts;

namespace KafkaTemplate.Messaging.Kafka.Consumer.Mapping
{
    public class MessageMapperProfile: Profile
    {
        public MessageMapperProfile() {
            CreateMap<AccountMessage, CreateAccountConsumerCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));
        }
    }
}
