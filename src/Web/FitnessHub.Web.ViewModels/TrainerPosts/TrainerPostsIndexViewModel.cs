namespace FitnessHub.Web.ViewModels.TrainerPosts
{
    using System.Collections.Generic;

    public class TrainerPostsIndexViewModel : PagingViewModel
    {
        public IEnumerable<TrainerPostViewModel> TrainerPosts { get; set; }
    }
}
