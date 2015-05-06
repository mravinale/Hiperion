namespace Hiperion.Infrastructure.Mappings
{
    using AutoMapper;

    using Domain;
    using Automapper;
    using Models;

    public class UserMappers : IObjectMapperConfigurator
    {
        public void Apply()
        {
            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<UserDto, User>();
        }
    }
}