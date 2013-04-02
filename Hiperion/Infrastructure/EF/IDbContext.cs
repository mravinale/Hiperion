namespace Hiperion.Infrastructure.EF
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Hiperion.Domain;

    public interface IDbContext
    {
        DbSet<TEntity> Entity<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
         

    }
}
