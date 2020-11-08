// ReSharper disable VirtualMemberCallInConstructor
namespace FitnessHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Services = new HashSet<Service>();
            this.Suplements = new HashSet<Suplement>();
            this.Equipments = new HashSet<Equipment>();
            this.Messages = new HashSet<Message>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<Suplement> Suplements { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
