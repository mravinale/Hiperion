using AutoMapper;

using WebApi.Domain;
using WebApi.Infrastructure.Automapper;
using WebApi.Models;

namespace WebApi.Infrastructure.Mappings
{
    public class UserMappers : IObjectMapperConfigurator
    {
        public void Apply()
        {
            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<UserDto, User>();
        }
    }
}