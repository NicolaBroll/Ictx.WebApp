using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public  ActionResult InternalServerError(string msg = "")
        {
            if(string.IsNullOrEmpty(msg))
                return StatusCode(500);

            return StatusCode(500, msg);
        }
    }
}
