using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _06_IS4_ApiServer.Controllers
{
    public class AuthController : Controller
    {
        [Route("/secret")]
        [Authorize]
        public string Secret() => "secret message from ApiServer";
    }
}
