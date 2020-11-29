namespace FitnessHub.Web.ViewModels.TrainerPosts
{
    using System;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainerPostViewModel : IMapFrom<TrainerPost>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
