namespace HRMSystemModel
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public int PositionID { get; set; }
        public DateOnly JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public bool WorkFromHome { get; set; }
        public bool NightShift { get; set; }
        public bool IsHourly { get; set; }
        public string BankAccountNumber { get; set; }

        public override string ToString()
        {
            return $"Employee[EmployeeID='{EmployeeID}'," +
                $" FullName='{FullName}'," +
                $" Position='{PositionID}'," +
                $" JoiningDate='{JoiningDate}'," +
                $" Salary=This string can't be logged as it carries sensitive information, sorry!," +
                $" WorkFromHome='{WorkFromHome}'," +
                $" NightShift='{NightShift}'," +
                $" IsHourly='{IsHourly}'," +
                $" BankAccountNumber=This string can't be logged as it carries sensitive information, sorry!]";
        }
    }
}