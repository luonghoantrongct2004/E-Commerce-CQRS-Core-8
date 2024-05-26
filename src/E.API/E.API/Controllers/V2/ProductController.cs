using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V2;


[ApiVersion("2.0")]
[ApiController]
[Route("api/v2/[controller]")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
