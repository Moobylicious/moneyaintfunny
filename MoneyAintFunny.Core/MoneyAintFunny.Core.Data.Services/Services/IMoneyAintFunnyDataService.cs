using MoneyAintFunny.Core.Dto.Models;
using System.Collections.Generic;

namespace MoneyAintFunny.Core.Data.Services.Services
{
    public interface IMoneyAintFunnyDataService
    {
        IEnumerable<TransactionRecordDto> GetTransactions();
    }
}
