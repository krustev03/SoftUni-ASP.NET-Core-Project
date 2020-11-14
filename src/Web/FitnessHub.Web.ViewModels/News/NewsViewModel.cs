namespace FitnessHub.Web.ViewModels.News
{
    using System;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
