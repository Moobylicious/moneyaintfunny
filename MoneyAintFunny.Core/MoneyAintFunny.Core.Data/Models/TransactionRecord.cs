using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyAintFunny.Data.Models
{
    public class TransactionRecord : SecuredEntity
    {
        public int Id { get; set; }

        //Note: for the purposes of this first cut, we're not concerned with collection TIME.
        //we only care about the date on which the collection happens.
        //Use sensibly named foreign key here.
        [ForeignKey("DateId")]
        public DateInfo DateInfo { get; set; }
        //Declare actual Foreign key - Date primary key is useful in itself without having to load the relation all the time.
        public int DateId { get; set; }

        public decimal TransactionAmount { get; set; }
        public TransactionDetail TransactionDetail {get;set;}
    }
}
