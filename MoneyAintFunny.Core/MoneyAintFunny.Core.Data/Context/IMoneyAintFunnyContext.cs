using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MoneyAintFunny.Core.Data.Context
{
    public interface IMoneyAintFunnyContext
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        EntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
