using System.Collections.Generic;

namespace HPowerTunings.Data.Models
{
    public class Supplier : BaseModel
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public int DeliveryRate { get; set; }

        public virtual ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}