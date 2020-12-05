namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainerPosts;

    public interface ITrainerPostService
    {
        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        T GetPostById<T>(int postId);

        int GetCount();

        Task AddPostAsync(TrainerPostInputModel model, string userId);

        Task EditPost(int trainerPostId, TrainerPostInputModel model);

        Task DeletePostByIdAsync(int trainerPostId);
    }
}
