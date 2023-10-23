namespace HRMSystemModel
{
    public class BonusAndDeduction
    {
        public int BonusID { get; set; }
        public int EmployeeID { get; set; }
        public string BonusType { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"BonusAndDeductions[BonusID='{BonusID}', EmployeeID='{EmployeeID}', BonusType='{BonusType}', Amount='{Amount}']";
        }
    }
}
