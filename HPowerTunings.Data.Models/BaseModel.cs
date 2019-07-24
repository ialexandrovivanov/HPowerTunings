using System;

namespace HPowerTunings.Data.Models
{
    public class BaseModel 
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
