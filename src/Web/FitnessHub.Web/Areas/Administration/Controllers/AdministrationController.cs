namespace FitnessHub.Web.Areas.Administration.Controllers
{
    using FitnessHub.Common;
    using FitnessHub.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
    }
}
