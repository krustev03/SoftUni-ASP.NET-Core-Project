namespace FitnessHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class EquipmentsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
