namespace Hiperion.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Hiperion.Domain;
    using Hiperion.Infrastructure.EF;

    public class UserRepository : IUserRepository
    {
        private readonly IDbContext context;

        public UserRepository(IDbContext context)
        {
            this.context = context;
        }

        public IList<User> GetAllValues()
        {
            return context.Entity<User>().ToList();
        }
         
        public void SaveOrUpdateUser(User user)
        {
            context.Entry(user).State = user.Id == 0? EntityState.Added : EntityState.Modified;
        }

        public void DeleteUser(int id)
        {
            var user = this.context.Entity<User>().Single(i=> i.Id == id);
            context.Entity<User>().Remove(user);
        }
    }
}