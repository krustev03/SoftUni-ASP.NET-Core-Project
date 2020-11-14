namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
