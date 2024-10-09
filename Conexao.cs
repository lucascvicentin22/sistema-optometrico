using MySqlConnector;
using System;
using System.Data;
using System.Configuration;
using MySql.Data;
using static iText.IO.Image.Jpeg2000ImageData;

public class Conexao
{
    private MySqlConnection conn;
    private string connectionString;

    public Conexao()
    {
        connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        conn = new MySqlConnection(connectionString);

    }
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
    public void OpenConnection()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }
    public DataTable ObterExamesPorCliente(int idCliente)
    {
        DataTable dt = new DataTable();
        string query = @"
SELECT 
    e.id_exame, 
    e.id_cliente, 
    cli.nome_completo, 
    e.data_do_exame, 
    c.nome AS nome_consultorio, 
    c.endereco, 
    c.cidade 
FROM 
    tb_exames e
JOIN 
    tb_consultorio c ON e.id_consultorio = c.id_consultorio
JOIN 
    tb_precadastro cli ON e.id_cliente = cli.id_cliente
WHERE 
    e.id_cliente = @idCliente
ORDER BY 
    e.data_do_exame DESC";

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar exames: " + ex.Message);
        }

        return dt; // Retorna o DataTable com os exames
    }

    public int ObterIdCliente(int idCliente)
    {
        int resultado = 0; // Valor padrão se não encontrado
        string query = "SELECT id_cliente FROM tb_precadastro WHERE id_cliente = @idCliente";

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    object res = cmd.ExecuteScalar();

                    // Verifica se o resultado não é nulo
                    if (res != null)
                    {
                        resultado = Convert.ToInt32(res); // Converte para int
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar ID do cliente: " + ex.Message);
        }

        return resultado; // Retorna o ID do cliente encontrado ou 0 se não encontrado
    }
    public DataTable ObterExames()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Consulta atualizada com join para obter dados da tabela tb_precadastro
            string query = @"
            SELECT 
                e.id_exame, 
                e.data_do_exame, 
                e.id_consultorio, 
                c.nome AS nome_consultorio, 
                c.endereco AS endereco_consultorio, 
                c.cidade AS cidade_consultorio,
                p.nome_completo, 
                p.nascimento, 
                p.idade, 
                p.profissao, 
                p.escolaridade
            FROM 
                tb_exames e
            JOIN 
                tb_consultorio c ON e.id_consultorio = c.id_consultorio
            JOIN 
                tb_precadastro p ON e.id_cliente = p.id_cliente"; // Junção correta com tb_precadastro

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter exames: " + ex.Message);
            }

            return dataTable;
        }
    }
    public DataTable BuscarDados(string query, params MySqlParameter[] parameters)
    {
        DataTable dataTable = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Adicionar os parâmetros ao comando
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }
        return dataTable;
    }

    public DataTable ObterNomesConsultorios()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT nome FROM tb_consultorio";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter nomes dos consultórios: " + ex.Message);
            }

            return dataTable;
        }
    }
    public DataTable ObterDadosConsultorio(string nomeConsultorio)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT id_consultorio, endereco, cidade FROM tb_consultorio WHERE nome = @nomeConsultorio";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nomeConsultorio", nomeConsultorio);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados do consultório: " + ex.Message);
            }

            return dataTable;
        }
    }
    public DataTable ObterDadosConsultorioPorNomeEEndereco(string nomeConsultorio, string enderecoConsultorio)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT id_consultorio, cidade FROM tb_consultorio WHERE nome = @nomeConsultorio AND endereco = @enderecoConsultorio";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nomeConsultorio", nomeConsultorio);
            command.Parameters.AddWithValue("@enderecoConsultorio", enderecoConsultorio);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados do consultório: " + ex.Message);
            }

            return dataTable;
        }
    }
    public void InserirPreCadastro(string nomeCompleto, string cpf, DateTime nascimento, int idade, string whatsapp, string rua, string bairro, string cidade, string profissao, string escolaridade, string sexo)
    {
        string query = "INSERT INTO tb_precadastro (nome_completo, cpf, nascimento, idade, whatsapp, rua, bairro, cidade, profissao, escolaridade, sexo) " +
                       "VALUES (@nome_completo, @cpf, @nascimento, @idade, @whatsapp, @rua, @bairro, @cidade, @profissao, @escolaridade, @sexo)";

        try
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nome_completo", nomeCompleto);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@nascimento", nascimento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@whatsapp", whatsapp);
                cmd.Parameters.AddWithValue("@rua", rua);
                cmd.Parameters.AddWithValue("@bairro", bairro);
                cmd.Parameters.AddWithValue("@cidade", cidade);
                cmd.Parameters.AddWithValue("@profissao", profissao);
                cmd.Parameters.AddWithValue("@escolaridade", escolaridade);
                cmd.Parameters.AddWithValue("@sexo", sexo);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao inserir dados no pré-cadastro: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
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
