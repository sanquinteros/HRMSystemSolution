using HRMSystemModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HRMSystemDataAccess
{
    public static class PaymentRepository
    {
        public static Payment? GetByPaymentID(int paymentID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT PaymentID, EmployeeID, PaymentDate, PaymentType, Amount, IsPix, IsAdvance " +
                    "FROM Payments WHERE PaymentID = @PaymentID";
                sqlCommand.Parameters.Add("@PaymentID", SqlDbType.Int).Value = paymentID;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PaymentsRepository.GetByPaymentID(). " +
                        $"Search term: PaymentID='{paymentID}'. Exception: {ex}.");
                    throw new Exception("GetByPaymentID Exception");
                }
            }
        }

        public static List<Payment> GetByEmployeeID(int employeeID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "SELECT * FROM Payments WHERE EmployeeID = @EmployeeID";
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                try
                {
                    return ExecuteReaderMultiple(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PaymentsRepository.GetByEmployeeID(). " +
                        $"Search term: EmployeeID='{employeeID}'. Exception: {ex}.");
                    throw new Exception("GetByEmployeeID Exception");
                }
            }
        }

        public static Payment? Insert(Payment payment)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Payments (EmployeeID, PaymentDate, PaymentType, Amount, IsPix, IsAdvance) " +
                                         "OUTPUT INSERTED.* " +
                                         "VALUES (@EmployeeID, @PaymentDate, @PaymentType, @Amount, @IsPix, @IsAdvance)";

                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = payment.EmployeeID;
                sqlCommand.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate;
                sqlCommand.Parameters.Add("@PaymentType", SqlDbType.VarChar, 50).Value = payment.PaymentType;
                sqlCommand.Parameters.Add("@Amount", SqlDbType.Decimal, 10).Value = payment.Amount;
                sqlCommand.Parameters.Add("@IsPix", SqlDbType.Bit).Value = payment.IsPix;
                sqlCommand.Parameters.Add("@IsAdvance", SqlDbType.Bit).Value = payment.IsAdvance;

                try
                {
                    return ExecuteReaderSingle(sqlCommand);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PaymentsRepository.Insert(). " +
                        $"Insert term: {payment}. Exception: {ex}.");
                    throw new Exception("Insert Exception");
                }
            }
        }

        public static int Update(Payment payment)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Payments SET EmployeeID = @EmployeeID, PaymentDate = @PaymentDate, PaymentType = @PaymentType, " +
                                         "Amount = @Amount, IsPix = @IsPix, IsAdvance = @IsAdvance WHERE PaymentID = @PaymentID";

                sqlCommand.Parameters.Add("@PaymentID", SqlDbType.Int).Value = payment.PaymentID;
                sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = payment.EmployeeID;
                sqlCommand.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate;
                sqlCommand.Parameters.Add("@PaymentType", SqlDbType.VarChar, 50).Value = payment.PaymentType;
                sqlCommand.Parameters.Add("@Amount", SqlDbType.Decimal, 10).Value = payment.Amount;
                sqlCommand.Parameters.Add("@IsPix", SqlDbType.Bit).Value = payment.IsPix;
                sqlCommand.Parameters.Add("@IsAdvance", SqlDbType.Bit).Value = payment.IsAdvance;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PaymentsRepository.Update(). " +
                        $"Update term: {payment}. Exception: {ex}.");
                    throw new Exception("Update Exception");
                }
            }
        }

        public static int Delete(int paymentID)
        {
            using SqlConnection conn = new Connection().GetOpenConnection();
            using (SqlCommand sqlCommand = conn.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Payments WHERE PaymentID = @PaymentID";
                sqlCommand.Parameters.Add("@PaymentID", SqlDbType.Int).Value = paymentID;

                try
                {
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"A SqlException occurred at PaymentsRepository.Delete(). " +
                        $"Delete term: PaymentID='{paymentID}'. Exception: {ex}.");
                    throw new Exception("Delete Exception");
                }
            }
        }

        private static Payment MapPaymentsFromDataReader(SqlDataReader reader)
        {
            return new Payment
            {
                PaymentID = (int)reader[0],
                EmployeeID = (int)reader[1],
                PaymentDate = (DateOnly)reader[2],
                PaymentType = (string)reader[3],
                Amount = (decimal)reader[4],
                IsPix = (bool)reader[5],
                IsAdvance = (bool)reader[6]
            };
        }

        private static Payment? ExecuteReaderSingle(SqlCommand sqlCommand)
        {
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapPaymentsFromDataReader(reader);
                }
            }
            return null;
        }

        private static List<Payment> ExecuteReaderMultiple(SqlCommand sqlCommand)
        {
            List<Payment> paymentList = new List<Payment>();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    paymentList.Add(MapPaymentsFromDataReader(reader));
                }
            }
            return paymentList;
        }
    }
}
