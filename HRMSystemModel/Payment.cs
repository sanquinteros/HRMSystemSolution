namespace HRMSystemModel
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int EmployeeID { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public bool IsPix { get; set; }
        public bool IsAdvance { get; set; }

        public override string ToString()
        {
            return $"Payments[PaymentID='{PaymentID}', EmployeeID='{EmployeeID}', PaymentDate='{PaymentDate}', PaymentType='{PaymentType}', Amount='{Amount}', IsPix='{IsPix}', IsAdvance='{IsAdvance}']";
        }
    }
}
