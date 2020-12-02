namespace FitnessHub.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static FitnessHub.Common.GlobalConstants;

    public class OrderInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public DateTime BirthDate => DateTime.ParseExact($"{this.BirthDay}/{this.BirthMonth}/{this.BirthYear}", @"d/M/yyyy", CultureInfo.InvariantCulture);

        [Required]
        public string City { get; set; }

        [Required]
        public string CityCode { get; set; }

        [Required]
        public string Adress { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
