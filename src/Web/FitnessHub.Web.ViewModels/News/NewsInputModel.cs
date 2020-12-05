namespace FitnessHub.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class NewsInputModel : IMapFrom<News>
    {
        [Required]
        [MinLength(NewsTitleMinLength, ErrorMessage = "The title must be at least 5 characters.")]
        [MaxLength(NewsTitleMaxLength)]
        [RegularExpression(NewsTitleRegex, ErrorMessage = "The title must start with capital letter.")]
        public string Title { get; set; }

        [Required]
        [MinLength(NewsContentMinLength, ErrorMessage = "The content must be at least 20 characters.")]
        [MaxLength(NewsContentMaxLength)]
        public string Content { get; set; }
    }
}
