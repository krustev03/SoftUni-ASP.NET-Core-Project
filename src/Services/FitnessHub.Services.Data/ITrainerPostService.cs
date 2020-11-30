namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.TrainerPosts;

    public interface ITrainerPostService
    {
        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        public int GetCount();

        public Task AddPostAsync(TrainerPostInputModel model, ApplicationUser appUser);

        public Task EditPost(int trainerPostId, TrainerPostInputModel model);

        public Task DeletePostByIdAsync(int trainerPostId);
    }
}
