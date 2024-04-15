using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        [Area("customer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
