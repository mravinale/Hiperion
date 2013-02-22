using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Services
{
    using AutoMapper;

    using WebApi.Domain;
    using WebApi.Domain.Repositories;
    using WebApi.Models;

    public class UserServices : IUserServices
    {
        private readonly IUserRepository repository;

        public UserServices(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = this.repository.GetAllValues();

            //add some bussines logic before update DB

            var userDtos = Mapper.Map<IList<User>, IList<UserDto>>(users);

            return userDtos;
        }

        public bool SaveOrUpdateUser(UserDto userDto)
        {
            var user = Mapper.Map<UserDto, User>(userDto);

            //add some bussines logic before update DB

            this.repository.SaveOrUpdateUser(user);

            return true;
        }

        public void DeleteUser(int id)
        {
           //add some bussines logic before update DB

            this.repository.DeleteUser(id);
        }


    }
}