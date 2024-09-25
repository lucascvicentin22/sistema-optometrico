using MySqlConnector;
using System;
using System.Data;
using System.Windows;

public class Conexao
{
    private MySqlConnection conn;
    private string connectionString;
    public Conexao()
    {
        connectionString = "Server = localhost; Database = db_clinica; User ID = root; Password = 2707; ";
        conn = new MySqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }

    public DataTable BuscarDados(string query)
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        return dt;
    }

    public void CloseConnection()
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }

    public void ExecuteQuery(string query, params MySqlParameter[] parametros)
    {
        try
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parametros);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar a query: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }
}
