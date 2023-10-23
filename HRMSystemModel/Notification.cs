namespace HRMSystemModel
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int EmployeeID { get; set; }
        public string Message { get; set; }
        public long DateSent { get; set; }

        public override string ToString()
        {
            return $"Notifications[NotificationID='{NotificationID}', EmployeeID='{EmployeeID}', Message='{Message}', DateSent='{DateSent}']";
        }
    }
}
