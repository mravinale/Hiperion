﻿namespace WebApi.Domain.Repositories
{
    using System.Collections.Generic;

    using WebApi.Domain;

    public interface IUserRepository
    {
        IList<User> GetAllValues();

        void SaveOrUpdateUser(User user);

        void DeleteUser(int id);
    }
}