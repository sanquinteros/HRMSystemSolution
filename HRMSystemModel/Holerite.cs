namespace HRMSystemModel
{
    public class Holerite
    {
        public int HoleriteID { get; set; }
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetAmount { get; set; }
        public byte[] Signature { get; set; }

        public override string ToString()
        {
            return $"Holerite[HoleriteID='{HoleriteID}', EmployeeID='{EmployeeID}', CompanyID='{CompanyID}', Month='{Month}', Year='{Year}', GrossAmount='{GrossAmount}', Deductions='{Deductions}', NetAmount='{NetAmount}']";
        }
    }
}
