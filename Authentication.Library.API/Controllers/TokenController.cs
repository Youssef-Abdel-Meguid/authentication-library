using Authentication.Library.API.App_Start;
using Authentication.Library.BLL;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Authentication.Library.API.Controllers
{
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        private readonly AuthenticationBLL _authenticationBLL;

        public TokenController()
        {
            _authenticationBLL = UnityConfig.AuthenticationBLL;
        }

        [HttpGet]
        [Route("gettoken")]
        public IHttpActionResult GetAuthenticationToken()
        {

            string systemName = "";

            if (Request.Headers.TryGetValues("System", out var systemValue))
            {
                if (string.IsNullOrEmpty(systemValue.ToString()))
                    return Content(HttpStatusCode.BadRequest, "Please provide system name");

                systemName = systemValue.ToList()[0];
            }

            return Ok(new { Token = _authenticationBLL.GenerateToken(systemName) });
        }
    }
}