using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    public abstract class BaseDataAccess
    {
        const string ConnectionString = @"Server=SNWIN10WK;Database=Northwind;Integrated Security=SSPI";
        protected SqlConnection connection;

        protected void CreateConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(ConnectionString);
            }
        }
        protected void OpenConnection()
        {
            CreateConnection();
            if(connection.State != ConnectionState.Open)
                connection.Open();
        }
        protected void CloseConnection()
        {
            if (connection != null)
            {
                if(connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
        protected SqlDataReader ExecuteQuery(
            string sqlCommand, 
            CommandType commandType, 
            params SqlParameter[] parameters)
        {
            var command = new SqlCommand(); 
            command.CommandType = commandType;
            command.CommandText = sqlCommand;
            if(parameters?.Length>0)
            {
                command.Parameters.AddRange(parameters);
            }
            command.Connection = connection;
            OpenConnection();
            return command.ExecuteReader();
        }
        
    }
}
