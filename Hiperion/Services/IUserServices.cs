namespace Hiperion.Services
{
    using System.Collections.Generic;

    using Hiperion.Models;

    public interface IUserServices
    {
        IEnumerable<UserDto> GetAllUsers();

        bool SaveOrUpdateUser(UserDto userDto);

        void DeleteUser(int id);
    }
}