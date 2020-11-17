namespace FitnessHub.Web.ViewModels.Orders
{
    using System;
    using System.Globalization;

    public class OrderInputModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public DateTime BirthDate => DateTime.ParseExact($"{this.BirthDay}/{this.BirthMonth}/{this.BirthYear}", @"d/M/yyyy", CultureInfo.InvariantCulture);

        public string City { get; set; }

        public string CityCode { get; set; }

        public string Adress { get; set; }

        public decimal Price { get; set; }
    }
}
