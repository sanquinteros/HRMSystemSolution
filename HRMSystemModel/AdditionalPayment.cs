namespace HRMSystemModel
{
    public class AdditionalPayment
    {
        public int AddPayID { get; set; }
        public int EmployeeID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"AdditionalPayments[AddPayID='{AddPayID}', EmployeeID='{EmployeeID}', Type='{Type}', Amount='{Amount}']";
        }
    }
}
