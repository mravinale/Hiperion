namespace Hiperion.Domain.Repositories
{
    using System.Collections.Generic;

    using Hiperion.Domain;

    public interface IUserRepository
    {
        IList<User> GetAllValues();

        void SaveOrUpdateUser(User user);

        void DeleteUser(int id);
    }
}