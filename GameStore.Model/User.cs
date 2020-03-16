using Microsoft.AspNet.Identity.EntityFramework;
using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace GameStore.Model
{
    public class User : IdentityUser
    {
        public override string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public override DateTime? LockoutEndDateUtc { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}
