using MoneyAintFunny.Core.Data.Context;
using MoneyAintFunny.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyAintFunny.Core.Data.Services
{
    public class MoneyAintFunnyQueryService : IMoneyAintFunnyQueryService
    {
        private readonly IMoneyAintFunnyContext _context;

        public MoneyAintFunnyQueryService(IMoneyAintFunnyContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetQueryable<T>() where T : BaseEntity
        {
            return _context.Set<T>().AsQueryable<T>();
        }
    }
}
