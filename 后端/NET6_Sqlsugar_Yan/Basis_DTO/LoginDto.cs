using System;

namespace Basis_DTO
{
    public class LoginDto
    {
        public LoginRequest Request { get; set; }
        public string InputCaptcha { get; set; }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
