using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TesteSolid.Classes
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        public Boolean Insert(Cliente cliente)
        {
            Boolean retorno = true;

            SqlConnection sql = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                sql.ConnectionString = "";

                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "";

                cmd.Parameters.AddWithValue("nome", cliente.nome);
                cmd.Parameters.AddWithValue("email", cliente.email);
                cmd.Parameters.AddWithValue("cpf", cliente.cpf);
                cmd.Parameters.AddWithValue("dataCriacao", cliente.dataCriacao);

                sql.Open();

                cmd.ExecuteNonQuery();

                sql.Close();
            }
            catch(Exception ex)
            {
                sql.Close();

                retorno = false;
            }
            finally
            {
                sql.Close();
            }

            return retorno;

        }

    }
}
