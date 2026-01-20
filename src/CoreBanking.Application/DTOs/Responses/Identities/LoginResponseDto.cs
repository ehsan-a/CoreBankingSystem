using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Responses.Identities
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
