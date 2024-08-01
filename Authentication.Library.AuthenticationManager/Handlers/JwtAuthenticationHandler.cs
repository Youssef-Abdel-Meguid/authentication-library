using Authentication.Library.AuthenticationManager.Contract;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Authentication.Library.AuthenticationManager.Handlers
{
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly string[] _anonymousPaths = new[] { "/api/token" };

        public JwtAuthenticationHandler(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var path = request.RequestUri.LocalPath.ToLowerInvariant();

            if (_anonymousPaths.Any(p => path.StartsWith(p)))
                return base.SendAsync(request, cancellationToken);

            if (!request.Headers.Authorization?.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase) ?? true)
                return Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized});

            var token = request.Headers.Authorization.Parameter;

            if (_authenticationManager.ValidateToken(token) == false)
                return Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized });

            return base.SendAsync(request, cancellationToken);
        }
    }
}
