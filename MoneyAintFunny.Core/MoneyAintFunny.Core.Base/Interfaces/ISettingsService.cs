using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyAintFunny.Core.Base.Interfaces
{
    public interface ISettingsService
    {
        string MainDatabaseConnectionString { get; }
        bool IsDesignTime { get; }
        string JwtSecret { get; }
        string SystemAdminRoleName { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        int TokenLifeTimeInMinutes { get; }
    }
}
