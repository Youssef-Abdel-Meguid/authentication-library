using System.Web.Http;


namespace Authentication.Library.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthenticationController : ApiController
    {

        [HttpGet]
        [Route("testtoken")]
        public IHttpActionResult TestToken() 
        {

            return Ok("Welcome");
        }
    }
}