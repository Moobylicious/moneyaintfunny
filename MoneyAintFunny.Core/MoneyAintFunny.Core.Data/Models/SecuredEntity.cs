using Microsoft.AspNetCore.Identity;

namespace MoneyAintFunny.Data.Models
{
    //This will be the base class used to enforce Row level security later.
    public class SecuredEntity : BaseEntity
    {
        public IdentityUser RecordSecurity { get; set; }
    }
}
