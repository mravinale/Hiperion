using AutoMapper;

using Hiperion.Domain;
using Hiperion.Infrastructure.Automapper;
using Hiperion.Models;

namespace Hiperion.Infrastructure.Mappings
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