using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Library.AuthenticationManager.Contract
{
    public interface IAuthenticationManager
    {
        string GenerateToken(string systemName);

        bool ValidateToken(string token);
    }
}
