using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Threading.Tasks;
using Common.DTOs;

namespace DataAccess
{
    public class AccessContext
    {
        private string connectionString { get; set; }
        private OleDbConnection connection { get; set; }

        public AccessContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["_accessDB"].ToString();
            connection = new OleDbConnection(connectionString);
        }

        public async Task InsertMultipleCodes(List<DiscountCardDto> discountCards)
        {
            try
            {
                connection.Open();
                foreach (var t in discountCards)
                {
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = $@"INSERT INTO Discounts(Code) values (@text)";
                    command.Parameters.Add("@text", OleDbType.VarChar).Value = t.DiscountCode;
                    await command.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        //should check if exists / is used
        private async Task<bool> CodeExists(string code)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $@"select ID, Code from Discounts where Code = {code}";

                var reader = await command.ExecuteReaderAsync();
                try
                {
                    return reader.HasRows;
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> UseCode(string code)
        {
            if (await CodeExists(code))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    command.CommandText = $"DELETE * from Discounts where Code = ?";

                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch(OleDbException dbException)
                {
                    throw new Exception(dbException.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return false;
        }
    }
}