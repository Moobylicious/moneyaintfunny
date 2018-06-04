namespace MoneyAintFunny.Data.Models
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
    }

    public enum TransactionType
    {
        AdHoc,
        RecurringMonthly,
        RecurringFourWeekly
    }
}