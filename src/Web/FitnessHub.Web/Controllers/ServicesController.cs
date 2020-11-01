namespace FitnessHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
