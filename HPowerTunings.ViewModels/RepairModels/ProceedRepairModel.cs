namespace HPowerTunings.ViewModels.RepairModels
{
    public class ProceedRepairModel
    {
        public In In { get; set; }
        public Out Out { get; set; }

        public ProceedRepairModel()
        {
            this.In = new In();
            this.Out = new Out();
        }
    }

    public class In
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string VinNumber { get; set; }

    }
    public class Out
    {

    }
}
