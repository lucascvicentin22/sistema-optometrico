using System;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

namespace SistemaOptometrico
{
    public partial class frmConsultorio : Form
    {
        private string connectionString = "Server=localhost;Database=db_clinica;User ID=root;Password=2707;";

        public frmConsultorio()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmConsultorio_Load); // Adiciona o manipulador de eventos para o evento Load
        }

        private void frmConsultorio_Load(object sender, EventArgs e)
        {
            CarregarDados(); // Carrega os dados quando o formulário é aberto
        }

        // Método para limpar os campos de entrada
        private void LimparCampos()
        {
            txtID.Text = string.Empty;
            txtNOME.Text = string.Empty;
            txtENDERECO.Text = string.Empty;
            txtCIDADE.Text = string.Empty;
            txtWHATSAPP.Text = string.Empty;
        }

        // Botão Salvar
        private void salvar_Click(object sender, EventArgs e)
        {
            // Primeiro, verifica se já existe um consultório com o mesmo endereço
            if (EnderecoExiste(txtENDERECO.Text))
            {
                MessageBox.Show("Já existe um consultório com esse endereço.");
                return;
            }

            // Se o endereço não existir, prossegue com a inserção
            string query = "INSERT INTO tb_consultorio (nome, endereco, cidade, whatsapp) VALUES (@nome, @endereco, @cidade, @whatsapp)";
            MySqlParameter[] parametros = {
        new MySqlParameter("@nome", txtNOME.Text),
        new MySqlParameter("@endereco", txtENDERECO.Text),
        new MySqlParameter("@cidade", txtCIDADE.Text),
        new MySqlParameter("@whatsapp", txtWHATSAPP.Text)
    };

            try
            {
                ExecuteQuery(query, parametros);
                MessageBox.Show("Cadastro salvo com sucesso!");
                CarregarDados(); // Recarrega os dados no DataGridView
                LimparCampos(); // Limpa os campos após salvar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        // Método para verificar se o endereço já existe
        private bool EnderecoExiste(string endereco)
        {
            string query = "SELECT COUNT(*) FROM tb_consultorio WHERE endereco = @endereco";
            MySqlParameter[] parametros = {
        new MySqlParameter("@endereco", endereco)
    };

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddRange(parametros);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Retorna true se o endereço já existir, false caso contrário
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar endereço: " + ex.Message);
                return false; // Retorna false em caso de erro
            }
        }


        // Botão Editar
        private void btnEDITAR_Click(object sender, EventArgs e)
        {
            string query = "UPDATE tb_consultorio SET nome = @nome, endereco = @endereco, cidade = @cidade, whatsapp = @whatsapp WHERE id_consultorio = @id_consultorio";
            MySqlParameter[] parametros = {
                new MySqlParameter("@nome", txtNOME.Text),
                new MySqlParameter("@endereco", txtENDERECO.Text),
                new MySqlParameter("@cidade", txtCIDADE.Text),
                new MySqlParameter("@whatsapp", txtWHATSAPP.Text),
                new MySqlParameter("@id_consultorio", txtID.Text) // Certifique-se de ter o ID do cliente carregado
            };

            try
            {
                ExecuteQuery(query, parametros);
                MessageBox.Show("Cadastro atualizado com sucesso!");
                CarregarDados();
                LimparCampos(); // Limpa os campos após editar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar: " + ex.Message);
            }
        }

        // Botão Apagar
        private void btnAPAGAR_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM tb_consultorio WHERE id_consultorio = @id_consultorio";
            MySqlParameter[] parametros = {
                new MySqlParameter("@id_consultorio", txtID.Text)
            };

            try
            {
                ExecuteQuery(query, parametros);
                MessageBox.Show("Cadastro excluído com sucesso!");
                CarregarDados();
                LimparCampos(); // Limpa os campos após deletar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message);
            }
        }

        // Evento de clique no DataGridView para carregar os dados nos campos
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["id_consultorio"].Value.ToString();
                txtNOME.Text = row.Cells["nome"].Value.ToString();
                txtENDERECO.Text = row.Cells["endereco"].Value.ToString();
                txtCIDADE.Text = row.Cells["cidade"].Value.ToString();
                txtWHATSAPP.Text = row.Cells["whatsapp"].Value.ToString();
            }
        }

        private void CarregarDados()
        {
            string query = "SELECT * FROM tb_consultorio";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Ajusta as colunas do DataGridView
                    dataGridView1.Columns["id_consultorio"].Visible = false; // Oculta a coluna ID
                    dataGridView1.Columns["nome"].HeaderText = "NOME"; // Altera o texto do cabeçalho
                    dataGridView1.Columns["endereco"].HeaderText = "ENDEREÇO";
                    dataGridView1.Columns["cidade"].HeaderText = "CIDADE";
                    dataGridView1.Columns["whatsapp"].HeaderText = "WHATSAPP";

                    // Ajusta automaticamente o tamanho das colunas
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Impede a adição de uma nova linha
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }


        // Método para executar comandos no banco de dados
        private void ExecuteQuery(string query, MySqlParameter[] parametros)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddRange(parametros);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar a query: " + ex.Message);
            }
        }
    }
}
