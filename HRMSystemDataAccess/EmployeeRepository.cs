using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class EmployeeRepository
    {
        public static Employee? GetById(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT EmployeeID, FullName, Position, JoiningDate, Salary, WorkFromHome, NightShift, IsHourly, BankAccountNumber " +
                                        "FROM Employee WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.GetById(). " +
                        $"Search term: Employee.Id='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetById Exception");
                }
            }
        }

        public static List<Employee> GetByFullName(string fullName)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT EmployeeID, FullName, Position, JoiningDate, Salary, WorkFromHome, NightShift, IsHourly, BankAccountNumber " +
                                        "FROM Employee WHERE FullName = @FullName";
                sqlCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 255).Value = fullName;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.GetByFullName(). " +
                        $"Search term: Employee.FullName='{fullName}'. Exception: {ex}.");
                    throw new Exception("GetByFullName Exception");
                }
            }
        }

        public static Employee? GetByBankAccountNumber(string bankAccountNumber)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT EmployeeID, FullName, Position, JoiningDate, Salary, WorkFromHome, NightShift, IsHourly, BankAccountNumber " +
                                        "FROM Employee WHERE BankAccountNumber = @BankAccountNumber";
                sqlCommand.Parameters.Add("@BankAccountNumber", SqlDbType.VarChar, 50).Value = bankAccountNumber;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.GetByBankAccountNumber(). " +
                        $"Search term: Employee.BankAccountNumber='{bankAccountNumber}'. Exception: {ex}.");
                    throw new Exception("GetByBankAccountNumber Exception");
                }
            }
        }

        public static Employee? Insert(Employee employee)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Employee (FullName, PositionID, JoiningDate, Salary, WorkFromHome, NightShift, IsHourly, BankAccountNumber) " +
                    "OUTPUT INSERTED.* " +
                    "VALUES (@FullName, @PositionID, @JoiningDate, @Salary, @WorkFromHome, @NightShift, @IsHourly, @BankAccountNumber); SELECT SCOPE_IDENTITY();";

                sqlCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 255).Value = employee.FullName;
                sqlCommand.Parameters.Add("@PositionID", SqlDbType.Int).Value = employee.PositionID;
                sqlCommand.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = employee.JoiningDate;
                sqlCommand.Parameters.Add("@Salary", SqlDbType.Decimal, 10).Value = employee.Salary;
                sqlCommand.Parameters.Add("@WorkFromHome", SqlDbType.Bit).Value = employee.WorkFromHome;
                sqlCommand.Parameters.Add("@NightShift", SqlDbType.Bit).Value = employee.NightShift;
                sqlCommand.Parameters.Add("@IsHourly", SqlDbType.Bit).Value = employee.IsHourly;
                sqlCommand.Parameters.Add("@BankAccountNumber", SqlDbType.VarChar, 50).Value = employee.BankAccountNumber;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.Insert(). " +
                        $"Insert term: {employee}. Exception: {ex}."); 
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Employee employee)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Employee SET FullName = @FullName, PositionId = @PositionId, JoiningDate = @JoiningDate, " +
                    "Salary = @Salary, WorkFromHome = @WorkFromHome, NightShift = @NightShift, " +
                    "IsHourly = @IsHourly, BankAccountNumber = @BankAccountNumber " +
                    "WHERE EmployeeID = @EmployeeID";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employee.EmployeeID;
                sqlCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 255).Value = employee.FullName;
                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = employee.PositionID;
                sqlCommand.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = employee.JoiningDate;
                sqlCommand.Parameters.Add("@Salary", SqlDbType.Decimal, 10).Value = employee.Salary;
                sqlCommand.Parameters.Add("@WorkFromHome", SqlDbType.Bit).Value = employee.WorkFromHome;
                sqlCommand.Parameters.Add("@NightShift", SqlDbType.Bit).Value = employee.NightShift;
                sqlCommand.Parameters.Add("@IsHourly", SqlDbType.Bit).Value = employee.IsHourly;
                sqlCommand.Parameters.Add("@BankAccountNumber", SqlDbType.VarChar, 50).Value = employee.BankAccountNumber;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.Update(). " +
                        $"Update term: {employee}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occoured at EmployeeRepository.Delete(). " +
                        $"Delete term: Employee.ID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Employee MapEmployeeFromDataReader(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeID = (int)reader[0],
                FullName = (string)reader[1],
                PositionID = (int)reader[2],
                JoiningDate = (DateOnly)reader[3],
                Salary = (decimal)reader[4],
                WorkFromHome = (bool)reader[5],
                NightShift = (bool)reader[6],
                IsHourly = (bool)reader[7],
                BankAccountNumber = (string)reader[8]
            };
        }

        private static Employee? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapEmployeeFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Employee> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    employeeList.Add(MapEmployeeFromDataReader(reader));
                }
            }
            return employeeList;
        }
    }
}
