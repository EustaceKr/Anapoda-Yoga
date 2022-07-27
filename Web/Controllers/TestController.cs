using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/test")]
    [ApiController]
    [Authorize]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult TestMethod()
        {
            return Ok("2xlalala");
        }
    }
}
