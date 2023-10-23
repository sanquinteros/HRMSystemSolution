namespace HRMSystemModel
{
    public class BonusOrDeduction
    {
        public int BonusOrDeductionID { get; set; }
        public int EmployeeID { get; set; }
        public string Type { get; set; }
        public bool IsEnabled { get; set; }
        public DateOnly CreatedDate { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"BonusOrDeduction[BonusOrDeductionID='{BonusOrDeductionID}', EmployeeID='{EmployeeID}', Type='{Type}', Type='{IsEnabled}', Type='{CreatedDate}', Amount='{Amount}']";
        }
    }
}
