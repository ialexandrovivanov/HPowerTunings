using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HPowerTunings.Data.Models
{
    public class Client : IdentityUser<string>
    {
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}