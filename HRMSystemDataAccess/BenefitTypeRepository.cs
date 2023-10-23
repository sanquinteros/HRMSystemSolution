using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class BenefitTypeRepository
    {
        public static BenefitType? GetById(int benefitTypeId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT BenefitTypeId, TypeName " +
                                        "FROM BenefitTypes WHERE BenefitTypeId = @BenefitTypeId";
                sqlCommand.Parameters.Add("@BenefitTypeId", SqlDbType.Int).Value = benefitTypeId;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BenefitTypeRepository.GetById()." +
                        $" Search term: BenefitTypeId='{benefitTypeId}'. Exception: {ex}.");
                    throw new Exception("GetById Exception");
                }
            }
        }

        public static List<BenefitType> GetByTypeName(string typeName)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT BenefitTypeId, TypeName " +
                                        "FROM BenefitTypes WHERE TypeName = @TypeName";
                sqlCommand.Parameters.Add("@TypeName", SqlDbType.VarChar, 255).Value = typeName;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BenefitTypeRepository.GetByTypeName()." +
                        $" Search term: TypeName='{typeName}'. Exception: {ex}.");
                    throw new Exception("GetByTypeName Exception");
                }
            }
        }

        public static BenefitType? Insert(BenefitType benefitType)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO BenefitTypes (TypeName) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@TypeName)";

                sqlCommand.Parameters.Add("@TypeName", SqlDbType.VarChar, 255).Value = benefitType.TypeName;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BenefitTypeRepository.Insert()." +
                        $" Insert term: {benefitType}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(BenefitType benefitType)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE BenefitTypes SET TypeName = @TypeName " +
                                         "WHERE BenefitTypeId = @BenefitTypeId";

                sqlCommand.Parameters.Add("@BenefitTypeId", SqlDbType.Int).Value = benefitType.BenefitTypeId;
                sqlCommand.Parameters.Add("@TypeName", SqlDbType.VarChar, 255).Value = benefitType.TypeName;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BenefitTypeRepository.Update()." +
                        $" Update term: {benefitType}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int benefitTypeId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM BenefitTypes WHERE BenefitTypeId = @BenefitTypeId";
                sqlCommand.Parameters.Add("@BenefitTypeId", SqlDbType.Int).Value = benefitTypeId;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at BenefitTypeRepository.Delete()." +
                        $" Delete term: BenefitTypeId='{benefitTypeId}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static BenefitType MapBenefitTypeFromDataReader(SqlDataReader reader)
        {
            return new BenefitType
            {
                BenefitTypeId = (int)reader["BenefitTypeId"],
                TypeName = (string)reader["TypeName"]
            };
        }

        private static BenefitType? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapBenefitTypeFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<BenefitType> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<BenefitType> listBenefitType = new List<BenefitType>();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listBenefitType.Add(MapBenefitTypeFromDataReader(reader));
                }
            }
            return listBenefitType;
        }
    }
}
