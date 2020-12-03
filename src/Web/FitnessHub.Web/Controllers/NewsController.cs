namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var viewModel = new NewsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.newsService.GetCount(),
                News = this.newsService.GetAllForPaging<NewsViewModel>(page, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(NewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.newsService.AddNewsAsync(model);

            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int newsId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int newsId, int page, NewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.newsService.EditNews(newsId, model);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int newsId, int page)
        {
            await this.newsService.DeleteNewsByIdAsync(newsId);

            return this.RedirectToAction("Index", new { page });
        }
    }
}
