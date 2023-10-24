using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class WorkHoursRepository
    {
        public static WorkHours? GetById(int entryId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT EntryID, EmployeeID, _Date, StartTime, EndTime, OvertimeHours, NightHours " +
                    "FROM WorkHours WHERE EntryID = @EntryID";
                sqlCommand.Parameters.Add("@EntryID", SqlDbType.Int).Value = entryId;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at WorkHoursRepository.GetById(). Search term: EntryID='{entryId}'. Exception: {ex}.");
                    throw new Exception("GetById Exception");
                }
            }
        }

        public static List<WorkHours> GetByEmployeeIdAndDate(int employeeId, DateTime date)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT EntryID, EmployeeID, _Date, StartTime, EndTime, OvertimeHours, NightHours " +
                    "FROM WorkHours WHERE EmployeeID = @EmployeeID AND _Date = @Date";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
                sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at WorkHoursRepository.GetByEmployeeIdAndDate(). Search terms: EmployeeID='{employeeId}', Date='{date}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeIdAndDate Exception");
                }
            }
        }

        public static WorkHours? Insert(WorkHours workHours)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO WorkHours (EmployeeID, _Date, StartTime, EndTime, OvertimeHours, NightHours) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @_Date, @StartTime, @EndTime, @OvertimeHours, @NightHours)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = workHours.EmployeeID;
                sqlCommand.Parameters.Add("@_Date", SqlDbType.Date).Value = workHours.Date;
                sqlCommand.Parameters.Add("@StartTime", SqlDbType.Time).Value = workHours.StartTime;
                sqlCommand.Parameters.Add("@EndTime", SqlDbType.Time).Value = workHours.EndTime;
                sqlCommand.Parameters.Add("@OvertimeHours", SqlDbType.Int).Value = workHours.OvertimeHours;
                sqlCommand.Parameters.Add("@NightHours", SqlDbType.Int).Value = workHours.NightHours;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at WorkHoursRepository.Insert(). Insert term: {workHours}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(WorkHours workHours)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE WorkHours SET EmployeeID = @EmployeeID, _Date = @_Date, StartTime = @StartTime, " +
                                         "EndTime = @EndTime, OvertimeHours = @OvertimeHours, NightHours = @NightHours " +
                                         "WHERE EntryID = @EntryID";

                sqlCommand.Parameters.Add("@EntryID", SqlDbType.Int).Value = workHours.EntryID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = workHours.EmployeeID;
                sqlCommand.Parameters.Add("@_Date", SqlDbType.Date).Value = workHours.Date;
                sqlCommand.Parameters.Add("@StartTime", SqlDbType.Time).Value = workHours.StartTime;
                sqlCommand.Parameters.Add("@EndTime", SqlDbType.Time).Value = workHours.EndTime;
                sqlCommand.Parameters.Add("@OvertimeHours", SqlDbType.Int).Value = workHours.OvertimeHours;
                sqlCommand.Parameters.Add("@NightHours", SqlDbType.Int).Value = workHours.NightHours;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at WorkHoursRepository.Update(). Update term: {workHours}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int entryId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM WorkHours WHERE EntryID = @EntryID";
                sqlCommand.Parameters.Add("@EntryID", SqlDbType.Int).Value = entryId;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at WorkHoursRepository.Delete(). Delete term: EntryID='{entryId}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static WorkHours MapWorkHoursFromDataReader(SqlDataReader reader)
        {
            return new WorkHours
            {
                EntryID = (int)reader[0],
                EmployeeID = (int)reader[1],
                Date = (DateTime)reader[2],
                StartTime = (TimeSpan)reader[3],
                EndTime = (TimeSpan)reader[4],
                OvertimeHours = (int)reader[5],
                NightHours = (int)reader[6]
            };
        }

        private static WorkHours? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapWorkHoursFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<WorkHours> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<WorkHours> listWorkHours = new List<WorkHours>();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listWorkHours.Add(MapWorkHoursFromDataReader(reader));
                }
            }
            return listWorkHours;
        }
    }
}
