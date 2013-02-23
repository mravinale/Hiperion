using System.Data.Entity;

using WebApi.Domain;
using WebApi.Infrastructure.EF;

namespace WebApi
{

    public class UserContext : DbContext, IDbContext
    {
        public UserContext(){ }

        public UserContext(string nameOrConnectionString) : base(nameOrConnectionString){}
        
        public DbSet<User> UserSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){}

        public DbSet<T> Entity<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}