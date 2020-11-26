namespace FitnessHub.Web.ViewModels.News
{
    using System.Collections.Generic;

    public class NewsIndexViewModel : PagingViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
