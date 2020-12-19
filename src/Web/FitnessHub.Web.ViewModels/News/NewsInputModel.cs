namespace FitnessHub.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class NewsInputModel : IMapFrom<News>
    {
        [Required]
        [MinLength(NewsTitleMinLength, ErrorMessage = NewsTitleMinLengthErrorMessage)]
        [MaxLength(NewsTitleMaxLength)]
        [RegularExpression(NewsTitleRegex, ErrorMessage = NewsTitleRegexErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(NewsContentMinLength, ErrorMessage = NewsContentMinLengthErrorMessage)]
        [MaxLength(NewsContentMaxLength)]
        public string Content { get; set; }
    }
}
