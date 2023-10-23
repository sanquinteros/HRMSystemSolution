namespace HRMSystemModel
{
    public class WorkHours
    {
        public int EntryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int OvertimeHours { get; set; }
        public int NightHours { get; set; }

        public override string ToString()
        {
            return $"WorkHours[EntryID='{EntryID}', EmployeeID='{EmployeeID}', Date='{Date}', StartTime='{StartTime}', EndTime='{EndTime}', OvertimeHours='{OvertimeHours}', NightHours='{NightHours}']";
        }
    }
}
