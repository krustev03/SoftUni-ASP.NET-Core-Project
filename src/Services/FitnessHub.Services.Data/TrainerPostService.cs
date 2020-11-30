namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.TrainerPosts;

    public class TrainerPostService : ITrainerPostService
    {
        private readonly IDeletableEntityRepository<TrainerPost> trainerPostsRepository;

        public TrainerPostService(
            IDeletableEntityRepository<TrainerPost> trainerPostsRepository)
        {
            this.trainerPostsRepository = trainerPostsRepository;
        }

        public async Task AddPostAsync(TrainerPostInputModel model, ApplicationUser appUser)
        {
            var post = new TrainerPost()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                AuthorId = appUser.Id,
            };

            await this.trainerPostsRepository.AddAsync(post);
            await this.trainerPostsRepository.SaveChangesAsync();
        }

        public async Task EditPost(int trainerPostId, TrainerPostInputModel model)
        {
            var post = this.trainerPostsRepository.All().Where(x => x.Id == trainerPostId).FirstOrDefault();

            post.FirstName = model.FirstName;
            post.LastName = model.LastName;
            post.Description = model.Description;

            this.trainerPostsRepository.Update(post);
            await this.trainerPostsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        {
            var posts = this.trainerPostsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return posts;
        }

        public int GetCount()
        {
            return this.trainerPostsRepository.All().Count();
        }

        public async Task DeletePostByIdAsync(int trainerPostId)
        {
            var post = this.trainerPostsRepository.All().Where(x => x.Id == trainerPostId).FirstOrDefault();
            this.trainerPostsRepository.Delete(post);
            await this.trainerPostsRepository.SaveChangesAsync();
        }
    }
}
