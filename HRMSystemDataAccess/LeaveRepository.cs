using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class LeaveRepository
    {
        public static Leave? GetByLeaveID(int leaveID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT LeaveID, EmployeeID, StartDate, EndDate, LeaveType, IsPaid, Documented " +
                                        "FROM Leave WHERE LeaveID = @LeaveID";
                sqlCommand.Parameters.Add("@LeaveID", SqlDbType.Int).Value = leaveID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at LeaveRepository.GetByLeaveID(). " +
                        $"Search term: LeaveID='{leaveID}'. Exception: {ex}.");
                    throw new Exception("GetByLeaveID Exception");
                }
            }
        }

        public static List<Leave> GetByEmployeeID(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT LeaveID, EmployeeID, StartDate, EndDate, LeaveType, IsPaid, Documented " +
                                        "FROM Leave WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at LeaveRepository.GetByEmployeeID(). " +
                        $"Search term: EmployeeID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeID Exception");
                }
            }
        }

        public static Leave? Insert(Leave leave)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Leave (EmployeeID, StartDate, EndDate, LeaveType, IsPaid, Documented) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @StartDate, @EndDate, @LeaveType, @IsPaid, @Documented)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = leave.EmployeeID;
                sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = leave.StartDate;
                sqlCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = leave.EndDate;
                sqlCommand.Parameters.Add("@LeaveType", SqlDbType.VarChar, 50).Value = leave.LeaveType;
                sqlCommand.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = leave.IsPaid;
                sqlCommand.Parameters.Add("@Documented", SqlDbType.Bit).Value = leave.Documented;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at LeaveRepository.Insert(). " +
                        $"Insert term: {leave}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Leave leave)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Leave SET EmployeeID = @EmployeeID, StartDate = @StartDate, EndDate = @EndDate, " +
                                         "LeaveType = @LeaveType, IsPaid = @IsPaid, Documented = @Documented " +
                                         "WHERE LeaveID = @LeaveID";

                sqlCommand.Parameters.Add("@LeaveID", SqlDbType.Int).Value = leave.LeaveID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = leave.EmployeeID;
                sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = leave.StartDate;
                sqlCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = leave.EndDate;
                sqlCommand.Parameters.Add("@LeaveType", SqlDbType.VarChar, 50).Value = leave.LeaveType;
                sqlCommand.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = leave.IsPaid;
                sqlCommand.Parameters.Add("@Documented", SqlDbType.Bit).Value = leave.Documented;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at LeaveRepository.Update(). " +
                        $"Update term: {leave}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int leaveID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Leave WHERE LeaveID = @LeaveID";
                sqlCommand.Parameters.Add("@LeaveID", SqlDbType.Int).Value = leaveID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at LeaveRepository.Delete(). " +
                        $"Delete term: LeaveID='{leaveID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Leave MapLeaveFromDataReader(SqlDataReader reader)
        {
            return new Leave
            {
                LeaveID = (int)reader[0],
                EmployeeID = (int)reader[1],
                StartDate = (DateOnly)reader[2],
                EndDate = (DateOnly)reader[3],
                LeaveType = (string)reader[4],
                IsPaid = (bool)reader[5],
                Documented = (bool)reader[6]
            };
        }

        private static Leave? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapLeaveFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Leave> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Leave> leaveList = new List<Leave>();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    leaveList.Add(MapLeaveFromDataReader(reader));
                }
            }
            return leaveList;
        }
    }
}
