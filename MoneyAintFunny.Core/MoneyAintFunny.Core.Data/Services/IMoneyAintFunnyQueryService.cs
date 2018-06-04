using MoneyAintFunny.Data.Models;
using System.Linq;

namespace MoneyAintFunny.Core.Data.Services
{
    public interface IMoneyAintFunnyQueryService
    {
        IQueryable<T> GetQueryable<T>() where T : BaseEntity;
    }
}
