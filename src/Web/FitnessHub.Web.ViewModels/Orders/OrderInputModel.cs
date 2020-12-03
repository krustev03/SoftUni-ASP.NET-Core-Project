namespace FitnessHub.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static FitnessHub.Common.GlobalConstants;

    public class OrderInputModel
    {
        [Required]
        [MinLength(BuyerNameMinLength, ErrorMessage = "The first name must be at least 2 characters.")]
        [RegularExpression(BuyerNameRegex, ErrorMessage = "The first name must start with capital letter.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(BuyerNameMinLength, ErrorMessage = "The last name must be at least 2 characters.")]
        [RegularExpression(BuyerNameRegex, ErrorMessage = "The last name must start with capital letter.")]
        public string LastName { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public DateTime BirthDate => DateTime.ParseExact($"{this.BirthDay}/{this.BirthMonth}/{this.BirthYear}", @"d/M/yyyy", CultureInfo.InvariantCulture);

        [Required]
        [MinLength(CityMinLength, ErrorMessage = "The city name must be at least 2 characters.")]
        public string City { get; set; }

        [Required]
        [RegularExpression(CityCodeRegex, ErrorMessage = "The city code is invalid.")]
        public string CityCode { get; set; }

        [Required]
        [MinLength(AdressMinLength, ErrorMessage = "The adress must be at least 5 characters.")]
        public string Adress { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
