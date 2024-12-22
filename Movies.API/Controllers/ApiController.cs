using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
