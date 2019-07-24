namespace HPowerTunings.Data.Models
{
    public class EmployeeRepair
    {
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        public string RepairId { get; set; }
        public virtual Repair Repair { get; set; }
    }
}
