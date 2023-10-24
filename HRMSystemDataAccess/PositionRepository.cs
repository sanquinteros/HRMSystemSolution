using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class PositionRepository
    {
        public static Position? GetByPositionId(int positionId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT PositionId, Name FROM Positions WHERE PositionId = @PositionId";
                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionId;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionRepository.GetByPositionId(). " +
                        $"Search term: PositionId='{positionId}'. Exception: {ex}.");
                    throw new Exception("GetByPositionId Exception");
                }
            }
        }

        public static Position? GetByName(string name)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT * FROM Positions WHERE Name = @Name";
                sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = name;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionRepository.GetByName(). " +
                        $"Search term: Name='{name}'. Exception: {ex}.");
                    throw new Exception("GetByName Exception");
                }
            }
        }

        public static Position? Insert(Position position)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Positions (Name) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@Name)";

                sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = position.Name;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionRepository.Insert(). " +
                        $"Insert term: {position}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Position position)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Positions SET Name = @Name WHERE PositionId = @PositionId";

                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = position.PositionId;
                sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = position.Name;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionRepository.Update(). " +
                        $"Update term: {position}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int positionId)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Positions WHERE PositionId = @PositionId";
                sqlCommand.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionId;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PositionRepository.Delete(). " +
                        $"Delete term: PositionId='{positionId}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Position MapPositionFromDataReader(SqlDataReader reader)
        {
            return new Position
            {
                PositionId = (int)reader[0],
                Name = (string)reader[1]
            };
        }

        private static Position? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapPositionFromDataReader(reader);
                }
            }
            return null;
        }
    }
}
