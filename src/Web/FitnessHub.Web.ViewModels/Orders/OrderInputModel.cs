namespace FitnessHub.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static FitnessHub.Common.GlobalConstants;

    public class OrderInputModel
    {
        [Required]
        [MinLength(BuyerNameMinLength, ErrorMessage = GeneralNameMinLengthErrorMessage)]
        [RegularExpression(BuyerNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(BuyerNameMinLength, ErrorMessage = GeneralNameMinLengthErrorMessage)]
        [RegularExpression(BuyerNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string LastName { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public DateTime BirthDate => DateTime.ParseExact($"{this.BirthDay}/{this.BirthMonth}/{this.BirthYear}", @"d/M/yyyy", CultureInfo.InvariantCulture);

        [Required]
        [MinLength(CityMinLength, ErrorMessage = CityNameMinLengthErrorMessage)]
        public string City { get; set; }

        [Required]
        [RegularExpression(CityCodeRegex, ErrorMessage = CityCodeRegexErrorMessage)]
        public string CityCode { get; set; }

        [Required]
        [MinLength(AdressMinLength, ErrorMessage = AdressMinLengthErrorMessage)]
        public string Adress { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
