﻿namespace MoneyAintFunny.Core.Dto.Models.Request
{
    public class CreateUserRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
