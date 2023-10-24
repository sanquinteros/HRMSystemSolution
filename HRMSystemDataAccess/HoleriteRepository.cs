using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class HoleriteRepository
    {
        public static Holerite? GetByHoleriteID(int holeriteID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT HoleriteID, EmployeeID, CompanyID, _Month, _Year, GrossAmount, Deductions, NetAmount, _Signature " +
                                        "FROM Holerite WHERE HoleriteID = @HoleriteID";
                sqlCommand.Parameters.Add("@HoleriteID", SqlDbType.Int).Value = holeriteID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.GetByHoleriteID(). " +
                        $"Search term: HoleriteID='{holeriteID}'. Exception: {ex}.");
                    throw new Exception("GetByHoleriteID Exception");
                }
            }
        }

        public static List<Holerite> GetByEmployeeID(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT HoleriteID, EmployeeID, CompanyID, _Month, _Year, GrossAmount, Deductions, NetAmount, _Signature " +
                                        "FROM Holerite WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.GetByEmployeeID(). " +
                        $"Search term: EmployeeID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeID Exception");
                }
            }
        }

        public static List<Holerite> GetByCompanyID(int companyID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT HoleriteID, EmployeeID, CompanyID, _Month, _Year, GrossAmount, Deductions, NetAmount, _Signature " +
                                        "FROM Holerite WHERE CompanyID = @CompanyID";
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.GetByCompanyID(). " +
                        $"Search term: CompanyID='{companyID}'. Exception: {ex}.");
                    throw new Exception("GetByCompanyID Exception");
                }
            }
        }

        public static Holerite? Insert(Holerite holerite)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Holerite (EmployeeID, CompanyID, _Month, _Year, GrossAmount, Deductions, NetAmount, _Signature) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @CompanyID, @_Month, @_Year, @GrossAmount, @Deductions, @NetAmount, @_Signature)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = holerite.EmployeeID;
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = holerite.CompanyID;
                sqlCommand.Parameters.Add("@_Month", SqlDbType.Int).Value = holerite.Month;
                sqlCommand.Parameters.Add("@_Year", SqlDbType.Int).Value = holerite.Year;
                sqlCommand.Parameters.Add("@GrossAmount", SqlDbType.Decimal, 10).Value = holerite.GrossAmount;
                sqlCommand.Parameters.Add("@Deductions", SqlDbType.Decimal, 10).Value = holerite.Deductions;
                sqlCommand.Parameters.Add("@NetAmount", SqlDbType.Decimal, 10).Value = holerite.NetAmount;
                sqlCommand.Parameters.Add("@_Signature", SqlDbType.VarBinary).Value = holerite.Signature;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.Insert(). " +
                        $"Insert term: {holerite}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Holerite holerite)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Holerite SET EmployeeID = @EmployeeID, CompanyID = @CompanyID, _Month = @_Month, _Year = @_Year, " +
                                         "GrossAmount = @GrossAmount, Deductions = @Deductions, NetAmount = @NetAmount, _Signature = @_Signature " +
                                         "WHERE HoleriteID = @HoleriteID";

                sqlCommand.Parameters.Add("@HoleriteID", SqlDbType.Int).Value = holerite.HoleriteID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = holerite.EmployeeID;
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = holerite.CompanyID;
                sqlCommand.Parameters.Add("@_Month", SqlDbType.Int).Value = holerite.Month;
                sqlCommand.Parameters.Add("@_Year", SqlDbType.Int).Value = holerite.Year;
                sqlCommand.Parameters.Add("@GrossAmount", SqlDbType.Decimal, 10).Value = holerite.GrossAmount;
                sqlCommand.Parameters.Add("@Deductions", SqlDbType.Decimal, 10).Value = holerite.Deductions;
                sqlCommand.Parameters.Add("@NetAmount", SqlDbType.Decimal, 10).Value = holerite.NetAmount;
                sqlCommand.Parameters.Add("@_Signature", SqlDbType.VarBinary).Value = holerite.Signature;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.Update(). " +
                        $"Update term: {holerite}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int holeriteID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Holerite WHERE HoleriteID = @HoleriteID";
                sqlCommand.Parameters.Add("@HoleriteID", SqlDbType.Int).Value = holeriteID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at HoleriteRepository.Delete(). " +
                        $"Delete term: HoleriteID='{holeriteID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Holerite MapHoleriteFromDataReader(SqlDataReader reader)
        {
            return new Holerite
            {
                HoleriteID = (int)reader[0],
                EmployeeID = (int)reader[1],
                CompanyID = (int)reader[2],
                Month = (int)reader[3],
                Year = (int)reader[4],
                GrossAmount = (decimal)reader[5],
                Deductions = (decimal)reader[6],
                NetAmount = (decimal)reader[7],
                Signature = !reader.IsDBNull(8) ? (byte[])reader["Signature"] : Array.Empty<byte>()
            };
        }

        private static Holerite? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapHoleriteFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Holerite> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Holerite> holeriteList = new List<Holerite>();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    holeriteList.Add(MapHoleriteFromDataReader(reader));
                }
            }
            return holeriteList;
        }
    }
}
