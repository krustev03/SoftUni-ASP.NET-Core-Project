namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult All()
        {
            var viewModel = new NewsIndexViewModel();
            viewModel.News = this.newsService.GetAllNews<NewsViewModel>();

            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddNewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.newsService.AddNewsAsync(model);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteNewsByIdAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
