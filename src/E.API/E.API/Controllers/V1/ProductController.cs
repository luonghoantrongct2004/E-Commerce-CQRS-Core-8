using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
