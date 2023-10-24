using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class NotificationRepository
    {
        public static Notification? GetByNotificationID(int notificationID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT NotificationID, EmployeeID, _Message, DateSent " +
                                        "FROM Notifications WHERE NotificationID = @NotificationID";
                sqlCommand.Parameters.Add("@NotificationID", SqlDbType.Int).Value = notificationID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at NotificationRepository.GetByNotificationID(). " +
                        $"Search term: NotificationID='{notificationID}'. Exception: {ex}.");
                    throw new Exception("GetByNotificationID Exception");
                }
            }
        }

        public static List<Notification> GetByEmployeeID(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT NotificationID, EmployeeID, _Message, DateSent " +
                                        "FROM Notifications WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at NotificationRepository.GetByEmployeeID(). " +
                        $"Search term: EmployeeID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeID Exception");
                }
            }
        }

        public static Notification? Insert(Notification notification)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Notifications (EmployeeID, _Message, DateSent) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @Message, @DateSent)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = notification.EmployeeID;
                sqlCommand.Parameters.Add("@Message", SqlDbType.Text).Value = notification.Message;
                sqlCommand.Parameters.Add("@DateSent", SqlDbType.BigInt).Value = notification.DateSent;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at NotificationRepository.Insert(). " +
                        $"Insert term: {notification}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Notification notification)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Notifications SET EmployeeID = @EmployeeID, _Message = @Message, DateSent = @DateSent " +
                                         "WHERE NotificationID = @NotificationID";

                sqlCommand.Parameters.Add("@NotificationID", SqlDbType.Int).Value = notification.NotificationID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = notification.EmployeeID;
                sqlCommand.Parameters.Add("@Message", SqlDbType.Text).Value = notification.Message;
                sqlCommand.Parameters.Add("@DateSent", SqlDbType.BigInt).Value = notification.DateSent;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at NotificationRepository.Update(). " +
                        $"Update term: {notification}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int notificationID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Notifications WHERE NotificationID = @NotificationID";
                sqlCommand.Parameters.Add("@NotificationID", SqlDbType.Int).Value = notificationID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at NotificationRepository.Delete(). " +
                        $"Delete term: NotificationID='{notificationID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Notification MapNotificationFromDataReader(SqlDataReader reader)
        {
            return new Notification
            {
                NotificationID = (int)reader[0],
                EmployeeID = (int)reader[1],
                Message = (string)reader[2],
                DateSent = (long)reader[3]
            };
        }

        private static Notification? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapNotificationFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Notification> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Notification> notificationList = new List<Notification>();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    notificationList.Add(MapNotificationFromDataReader(reader));
                }
            }
            return notificationList;
        }
    }
}
