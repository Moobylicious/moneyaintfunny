namespace MoneyAintFunny.Data.Models
{
    public class BaseEntity
    {
        //record version used for updates when upserting data.
        public int Version { get; set; }
        //Not sure we'd need this really
        //public DateTime CommitDateTime{get;set;}
    }
}
