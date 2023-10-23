﻿namespace HRMSystemModel
{
    public class Leave
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public string LeaveType { get; set; }
        public bool IsPaid { get; set; }
        public bool Documented { get; set; }

        public override string ToString()
        {
            return $"Leave[LeaveID='{LeaveID}', EmployeeID='{EmployeeID}', StartDate='{StartDate}', EndDate='{EndDate}', LeaveType='{LeaveType}', IsPaid='{IsPaid}', Documented='{Documented}']";
        }
    }
}
