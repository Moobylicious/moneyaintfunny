using System;

namespace MoneyAintFunny.Core.Dto.Models
{
    public class TransactionRecordDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public TransactionDetailDto Details { get; set; }
    }
}
