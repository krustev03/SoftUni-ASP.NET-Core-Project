﻿namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class TrainerPost : BaseDeletableModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
