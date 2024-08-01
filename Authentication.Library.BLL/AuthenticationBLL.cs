using Authentication.Library.AuthenticationManager.Contract;
using Authentication.Library.AuthenticationManager.Models;

namespace Authentication.Library.BLL
{
    public class AuthenticationBLL
    {
        private readonly IAuthenticationManager _authenticationManager;
        private JwtTokenConfiguration JwtTokenConfiguration;

        public AuthenticationBLL(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public string GenerateToken(string systemName)
        {
            return _authenticationManager.GenerateToken(systemName);
        }
    }
}
