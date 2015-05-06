namespace Hiperion.Infrastructure.EF
{
    using System.Data.Entity;
    using Domain;

    public class UserContext : DbContext, IDbContext
    {
        public UserContext(){ }

        public UserContext(string nameOrConnectionString) : base(nameOrConnectionString){}
        
        public DbSet<User> UserSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){}

        public DbSet<T> Entity<T>() where T : class
        {
            return Set<T>();
        }
    }
}