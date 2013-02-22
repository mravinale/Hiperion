namespace WebApi.Services
{
    using System.Collections.Generic;

    using WebApi.Models;

    public interface IUserServices
    {
        IEnumerable<UserDto> GetAllUsers();

        bool SaveOrUpdateUser(UserDto userDto);

        void DeleteUser(int id);
    }
}