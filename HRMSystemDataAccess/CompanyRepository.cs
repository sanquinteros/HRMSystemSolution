using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class CompanyRepository
    {

        public static Company? GetById(int companyID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT CompanyID, CompanyName, TradingName, _Address, CNPJ " +
                                        "FROM Company WHERE CompanyID = @CompanyID";
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.GetById()." +
                        $" Search term: CompanyID='{companyID}'. Exception: {ex}.");
                    throw new Exception("GetById Exception");
                }
            }
        }

        public static Company? GetByCNPJ(string cnpj)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT CompanyID, CompanyName, TradingName, _Address, CNPJ " +
                                        "FROM Company WHERE CNPJ = @CNPJ";
                sqlCommand.Parameters.Add("@CNPJ", SqlDbType.VarChar, 18).Value = cnpj;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.GetByCNPJ()." +
                        $" Search term: CNPJ='{cnpj}'. Exception: {ex}.");
                    throw new Exception("GetByCNPJ Exception");
                }
            }
        }

        public static List<Company> GetByCompanyName(string companyName)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT CompanyID, CompanyName, TradingName, _Address, CNPJ " +
                                        "FROM Company WHERE CompanyName = @CompanyName";
                sqlCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 255).Value = companyName;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.GetByCompanyName()." +
                        $" Search term: CompanyName='{companyName}'. Exception: {ex}.");
                    throw new Exception("GetByCompanyName Exception");
                }
            }
        }

        public static Company? Insert(Company company)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Company (CompanyName, TradingName, _Address, CNPJ) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@CompanyName, @TradingName, @_Address, @CNPJ)";

                sqlCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 255).Value = company.CompanyName;
                sqlCommand.Parameters.Add("@TradingName", SqlDbType.VarChar, 255).Value = company.TradingName;
                sqlCommand.Parameters.Add("@_Address", SqlDbType.VarChar, 255).Value = company.Address;
                sqlCommand.Parameters.Add("@CNPJ", SqlDbType.VarChar, 18).Value = company.CNPJ;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.Insert()." +
                        $" Insert term: {company}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Company company)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Company SET CompanyName = @CompanyName, TradingName = @TradingName, _Address = @_Address, CNPJ = @CNPJ " +
                                         "WHERE CompanyID = @CompanyID";

                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = company.CompanyID;
                sqlCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 255).Value = company.CompanyName;
                sqlCommand.Parameters.Add("@TradingName", SqlDbType.VarChar, 255).Value = company.TradingName;
                sqlCommand.Parameters.Add("@_Address", SqlDbType.VarChar, 255).Value = company.Address;
                sqlCommand.Parameters.Add("@CNPJ", SqlDbType.VarChar, 18).Value = company.CNPJ;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.Update()." +
                        $" Update term: {company}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int companyID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Company WHERE CompanyID = @CompanyID";
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at CompanyRepository.Delete()." +
                        $" Delete term: CompanyID='{companyID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Company MapCompanyFromDataReader(SqlDataReader reader)
        {
            return new Company
            {
                CompanyID = (int)reader["CompanyID"],
                CompanyName = (string)reader["CompanyName"],
                TradingName = (string)reader["TradingName"],
                Address = (string)reader["_Address"],
                CNPJ = (string)reader["CNPJ"]
            };
        }

        private static Company? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapCompanyFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Company> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Company> companyList = new List<Company>();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    companyList.Add(MapCompanyFromDataReader(reader));
                }
            }
            return companyList;
        }
    }
}
