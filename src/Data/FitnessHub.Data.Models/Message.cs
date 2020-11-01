namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Content { get; set; }
    }
}
