using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class PositionBenefitRepository
    {
        public static PositionBenefit? GetByPositionBenefitId(int positionBenefitId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT PositionBenefitId, PositionId, BenefitTypeId, _Value " +
                    "FROM PositionBenefits WHERE PositionBenefitId = @PositionBenefitId";
                sqlCommand.Parameters.Add("@PositionBenefitId", SqlDbType.Int).Value = positionBenefitId;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionBenefitRepository.GetByPositionBenefitId(). " +
                        $"Search term: PositionBenefitId='{positionBenefitId}'. Exception: {ex}.");
                    throw new Exception("GetByPositionBenefitId Exception");
                }
            }
        }

        public static List<PositionBenefit> GetByPositionId(int positionId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT PositionBenefitId, PositionId, BenefitTypeId, _Value " +
                    "FROM PositionBenefits WHERE PositionId = @PositionId";
                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionId;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionBenefitRepository.GetByPositionId(). " +
                        $"Search term: PositionId='{positionId}'. Exception: {ex}.");
                    throw new Exception("GetByPositionId Exception");
                }
            }
        }

        public static PositionBenefit? Insert(PositionBenefit positionBenefit)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO PositionBenefits (PositionId, BenefitTypeId, _Value) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@PositionId, @BenefitTypeId, @Value)";

                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionBenefit.PositionId;
                sqlCommand.Parameters.Add("@BenefitTypeId", SqlDbType.Int).Value = positionBenefit.BenefitTypeId;
                sqlCommand.Parameters.Add("@Value", SqlDbType.Decimal, 10).Value = positionBenefit.Value;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionBenefitRepository.Insert(). " +
                        $"Insert term: {positionBenefit}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(PositionBenefit positionBenefit)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE PositionBenefits SET PositionId = @PositionId, BenefitTypeId = @BenefitTypeId, _Value = @Value WHERE PositionBenefitId = @PositionBenefitId";

                sqlCommand.Parameters.Add("@PositionBenefitId", SqlDbType.Int).Value = positionBenefit.PositionBenefitId;
                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionBenefit.PositionId;
                sqlCommand.Parameters.Add("@BenefitTypeId", SqlDbType.Int).Value = positionBenefit.BenefitTypeId;
                sqlCommand.Parameters.Add("@Value", SqlDbType.Decimal, 10).Value = positionBenefit.Value;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionBenefitRepository.Update(). " +
                        $"Update term: {positionBenefit}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int positionBenefitId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM PositionBenefits WHERE PositionBenefitId = @PositionBenefitId";
                sqlCommand.Parameters.Add("@PositionBenefitId", SqlDbType.Int).Value = positionBenefitId;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionBenefitRepository.Delete(). " +
                        $"Delete term: PositionBenefitId='{positionBenefitId}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static PositionBenefit MapPositionBenefitFromDataReader(SqlDataReader reader)
        {
            return new PositionBenefit
            {
                PositionBenefitId = (int)reader[0],
                PositionId = (int)reader[1],
                BenefitTypeId = (int)reader[2],
                Value = (decimal)reader[3]
            };
        }
        private static PositionBenefit? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return MapPositionBenefitFromDataReader(reader);
            }
            return null;
        }

        private static List<PositionBenefit> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<PositionBenefit> positionBenefitList = new List<PositionBenefit>();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                positionBenefitList.Add(MapPositionBenefitFromDataReader(reader));
            }
            return positionBenefitList;
        }
    }
}
