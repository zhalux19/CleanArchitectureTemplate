using AutoMapper;
using KafkaTemplate.Core.Entities;
using KafkaTemplate.Data.DataModels;
using MongoDB.Bson;

namespace KafkaTemplate.Data.Mapping
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<Account, AccountData>()
                .ForMember(dest => dest._id, opt => {
                    opt.PreCondition(src => src.Id != null);
                    opt.MapFrom(src => ObjectId.Parse(src.Id));
                });

            CreateMap<AccountData, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));
        }
    }
}
