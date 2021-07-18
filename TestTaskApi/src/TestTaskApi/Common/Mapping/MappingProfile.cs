using AutoMapper;
using TestTaskApi.Data.Entities;
using TestTaskApi.Models;

namespace TestTaskApi.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountEntity, AccountModel>().ReverseMap();
            CreateMap<RoleEntity, RoleModel>().ReverseMap();
            CreateMap<MessageEntity, MessageModel>().ReverseMap();
        }
    }
}
