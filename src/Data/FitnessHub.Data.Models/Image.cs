namespace FitnessHub.Data.Models
{
    using System;

    using FitnessHub.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string PublicId { get; set; }

        public int? EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int? SuplementId { get; set; }

        public virtual Suplement Suplement { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
