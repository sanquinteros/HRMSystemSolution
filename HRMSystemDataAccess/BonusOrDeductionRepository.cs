using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class BonusOrDeductionRepository
    {
        public static BonusOrDeduction? GetByBonusOrDeductionID(int bonusOrDeductionId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT BonusOrDeductionID, EmployeeID, IsEnabled, CreatedDate, _Type, Amount " +
                                        "FROM BonusesOrDeductions WHERE BonusOrDeductionID = @BonusOrDeductionID";
                sqlCommand.Parameters.Add("@BonusOrDeductionID", SqlDbType.Int).Value = bonusOrDeductionId;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BonusAndDeductionRepository.GetByBonusOrDeductionID(). " +
                        $"Search term: BonusOrDeductionID='{bonusOrDeductionId}'. Exception: {ex}.");
                    throw new Exception("GetByBonusOrDeductionID Exception");
                }
            }
        }

        public static List<BonusOrDeduction> GetByEmployeeID(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT BonusOrDeductionID, EmployeeID, IsEnabled, CreatedDate, _Type, Amount " +
                                        "FROM BonusesOrDeductions WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BonusAndDeductionRepository.GetByEmployeeID(). " +
                        $"Search term: EmployeeID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeID Exception");
                }
            }
        }

        public static BonusOrDeduction? Insert(BonusOrDeduction bonus)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO BonusesOrDeductions (EmployeeID, IsEnabled, CreatedDate, _Type, Amount) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @IsEnabled, @CreatedDate, @Type, @Amount)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = bonus.EmployeeID;
                sqlCommand.Parameters.Add("@IsEnabled", SqlDbType.Bit).Value = bonus.IsEnabled;
                sqlCommand.Parameters.Add("@CreatedDate", SqlDbType.Date).Value = bonus.CreatedDate;
                sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar, 100).Value = bonus.Type;
                sqlCommand.Parameters.Add("@Amount", SqlDbType.Decimal, 10).Value = bonus.Amount;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BonusAndDeductionRepository.Insert(). " +
                        $"Insert term: {bonus}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(BonusOrDeduction bonus)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE BonusesOrDeductions SET EmployeeID = @EmployeeID, IsEnabled = @IsEnabled, " +
                                         "CreatedDate = @CreatedDate, _Type = @Type, Amount = @Amount " +
                                         "WHERE BonusOrDeductionID = @BonusOrDeductionID";

                sqlCommand.Parameters.Add("@BonusOrDeductionID", SqlDbType.Int).Value = bonus.BonusOrDeductionID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = bonus.EmployeeID;
                sqlCommand.Parameters.Add("@IsEnabled", SqlDbType.Bit).Value = bonus.IsEnabled;
                sqlCommand.Parameters.Add("@CreatedDate", SqlDbType.Date).Value = bonus.CreatedDate;
                sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar, 100).Value = bonus.Type;
                sqlCommand.Parameters.Add("@Amount", SqlDbType.Decimal, 10).Value = bonus.Amount;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BonusAndDeductionRepository.Update(). " +
                        $"Update term: {bonus}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int bonusOrDeductionId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM BonusesOrDeductions WHERE BonusOrDeductionID = @BonusOrDeductionID";
                sqlCommand.Parameters.Add("@BonusOrDeductionID", SqlDbType.Int).Value = bonusOrDeductionId;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BonusAndDeductionRepository.Delete(). " +
                        $"Delete term: BonusOrDeductionID='{bonusOrDeductionId}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static BonusOrDeduction MapBonusOrDeductionFromDataReader(SqlDataReader reader)
        {
            return new BonusOrDeduction
            {
                BonusOrDeductionID = (int)reader[0],
                EmployeeID = (int)reader[1],
                IsEnabled = (bool)reader[2],
                CreatedDate = (DateOnly)reader[3],
                Type = (string)reader[4],
                Amount = (decimal)reader[5]
            };
        }

        private static BonusOrDeduction? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapBonusOrDeductionFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<BonusOrDeduction> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<BonusOrDeduction> bonusOrDeductionList = new List<BonusOrDeduction>();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    bonusOrDeductionList.Add(MapBonusOrDeductionFromDataReader(reader));
                }
            }
            return bonusOrDeductionList;
        }
    }
}
