using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
