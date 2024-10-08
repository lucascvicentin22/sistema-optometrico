using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySqlConnector;

namespace SistemaOptometrico
{
    public partial class frmFichaAtendimento : Form
    {
        private Conexao conexao = new Conexao();
        private int selectedId; // Para armazenar o ID do registro selecionado

        public frmFichaAtendimento()
        {
            InitializeComponent();
            LoadData(); // Carregar dados ao inicializar o formulário
        }

        private void LoadData()
        {
            using (MySqlConnection connection = conexao.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM tb_precadastro";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["id_cliente"].HeaderText = "ID";
                    dataGridView1.Columns["nome_completo"].HeaderText = "NOME COMPLETO";
                    dataGridView1.Columns["cpf"].HeaderText = "CPF";
                    dataGridView1.Columns["nascimento"].HeaderText = "NASCIMENTO";
                    dataGridView1.Columns["idade"].HeaderText = "IDADE";
                    dataGridView1.Columns["whatsapp"].HeaderText = "WHATSAPP";
                    dataGridView1.Columns["rua"].HeaderText = "RUA";
                    dataGridView1.Columns["bairro"].HeaderText = "BAIRRO";
                    dataGridView1.Columns["cidade"].HeaderText = "CIDADE";
                    dataGridView1.Columns["profissao"].HeaderText = "PROFISSÃO";
                    dataGridView1.Columns["escolaridade"].HeaderText = "ESCOLARIDADE";
                    dataGridView1.Columns["sexo"].HeaderText = "SEXO";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Verificar se a célula clicada é a célula do ID
                if (e.ColumnIndex == dataGridView1.Columns["id_cliente"].Index)
                {
                    Clipboard.SetText(row.Cells["id_cliente"].Value.ToString());
                    MessageBox.Show("ID copiado para a área de transferência.");
                }

                // Preencher os campos com dados da linha selecionada
                txtIDCLIENTE.Text = row.Cells["id_cliente"].Value.ToString();
                txtNOMECOMPLETO.Text = row.Cells["nome_completo"].Value.ToString();
                txtCPF.Text = row.Cells["cpf"].Value.ToString();
                txtNASCIMENTO.Text = row.Cells["nascimento"].Value.ToString();
                txtIDADE.Text = row.Cells["idade"].Value.ToString();
                txtWHATSAPP.Text = row.Cells["whatsapp"].Value.ToString();
                txtRUA.Text = row.Cells["rua"].Value.ToString();
                txtBAIRRO.Text = row.Cells["bairro"].Value.ToString();
                txtCIDADE.Text = row.Cells["cidade"].Value.ToString();
                txtPROFISSAO.Text = row.Cells["profissao"].Value.ToString();
                txtESCOLARIDADE.Text = row.Cells["escolaridade"].Value.ToString();
                txtSEXO.Text = row.Cells["sexo"].Value.ToString();


                // Armazenar o ID do registro selecionado
                selectedId = Convert.ToInt32(row.Cells["id_cliente"].Value);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Selecione um registro para editar.");
                return;
            }

            DateTime nascimento;
            if (!DateTime.TryParseExact(txtNASCIMENTO.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nascimento))
            {
                MessageBox.Show("Formato de data incorreto. Use dd/MM/yyyy.");
                return;
            }

            string nascimentoFormatado = nascimento.ToString("yyyy-MM-dd");

            using (MySqlConnection connection = conexao.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"
                        UPDATE tb_precadastro SET 
                        nome_completo = @nome_completo,
                        cpf = @cpf,
                        nascimento = @nascimento,
                        idade = @idade,
                        whatsapp = @whatsapp,
                        rua = @rua,
                        bairro = @bairro,
                        cidade = @cidade,
                        profissao = @profissao,
                        escolaridade = @escolaridade,
                        sexo = @sexo
                        WHERE id_cliente = @id_cliente";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nome_completo", txtNOMECOMPLETO.Text);
                    command.Parameters.AddWithValue("@cpf", txtCPF.Text);
                    command.Parameters.AddWithValue("@nascimento", nascimentoFormatado);
                    command.Parameters.AddWithValue("@idade", txtIDADE.Text);
                    command.Parameters.AddWithValue("@whatsapp", txtWHATSAPP.Text);
                    command.Parameters.AddWithValue("@rua", txtRUA.Text);
                    command.Parameters.AddWithValue("@bairro", txtBAIRRO.Text);
                    command.Parameters.AddWithValue("@cidade", txtCIDADE.Text);
                    command.Parameters.AddWithValue("@profissao", txtPROFISSAO.Text);
                    command.Parameters.AddWithValue("@escolaridade", txtESCOLARIDADE.Text);
                    command.Parameters.AddWithValue("@sexo", txtSEXO.Text);
                    command.Parameters.AddWithValue("@id_cliente", selectedId);

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Registro atualizado com sucesso." : "Nenhum registro atualizado.");
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar dados: {ex.Message}");
                }
            }
        }


        private void ClearFields()
        {
            // Limpar todos os campos
            txtIDCLIENTE.Clear();
            txtNOMECOMPLETO.Clear();
            txtCPF.Clear();
            txtNASCIMENTO.Clear();
            txtIDADE.Clear();
            txtWHATSAPP.Clear();
            txtRUA.Clear();
            txtBAIRRO.Clear();
            txtCIDADE.Clear();
            txtPROFISSAO.Clear();
            txtESCOLARIDADE.Clear();
            txtSEXO.Clear();
            selectedId = 0; // Resetar o ID selecionado
        }

        private void btnJaTemCadastro_Click(object sender, EventArgs e)
        {
            frmNovoCadastroFichaTendimento NovaFicha = new frmNovoCadastroFichaTendimento();
            NovaFicha.ShowDialog();
        }

        private void btnNovoCadastro_Click_1(object sender, EventArgs e)
        {
            frmPreCadastro cadastropessoal = new frmPreCadastro();
            cadastropessoal.ShowDialog();
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            string pesquisa = txtPesquisar.Text.Trim();
            using (MySqlConnection connection = conexao.GetConnection())

            {
                try
                {
                    connection.Open();
                    // Modifica a consulta para pesquisar tanto por nome quanto por CPF
                    string query = $"SELECT * FROM tb_precadastro WHERE nome_completo LIKE '%{pesquisa}%' OR cpf LIKE '%{pesquisa}%'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao pesquisar dados: {ex.Message}");
                }
            }
        }

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            // Validar se o número de WhatsApp está preenchido
            if (string.IsNullOrWhiteSpace(txtWHATSAPP.Text))
            {
                MessageBox.Show("O campo de WhatsApp está vazio.");
                return;
            }

            // Obter o número de WhatsApp do TextBox
            string numeroWhatsApp = txtWHATSAPP.Text.Trim();

            // Validar o formato do número (deve estar no formato +55DDDDDDDDDD ou DDDDDDDDDD)
            // Este exemplo assume que o número pode estar no formato com ou sem o código do país
            if (!System.Text.RegularExpressions.Regex.IsMatch(numeroWhatsApp, @"^\+55\d{11}$|^\d{11}$"))
            {
                MessageBox.Show("O número de WhatsApp deve estar no formato brasileiro com 11 dígitos.");
                return;
            }

            // Remover caracteres não numéricos
            string numeroFormatado = new string(numeroWhatsApp.Where(char.IsDigit).ToArray());

            // Construir a URL do WhatsApp Web
            string url = $"https://wa.me/{numeroFormatado}";

            // Abrir a URL no navegador padrão
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir o WhatsApp Web: {ex.Message}");
            }
        }
        private void AtualizarTodasAsIdades()
        {
            using (MySqlConnection connection = conexao.GetConnection())
            {
                connection.Open();

                string selectQuery = "SELECT id_cliente, nascimento FROM tb_precadastro";
                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, connection))
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    List<(int idCliente, int idade)> dadosParaAtualizar = new List<(int idCliente, int idade)>();

                    while (reader.Read())
                    {
                        int idCliente = reader.GetInt32("id_cliente");
                        DateTime dataNascimento = reader.GetDateTime("nascimento");
                        int idade = CalcularIdade(dataNascimento);
                        dadosParaAtualizar.Add((idCliente, idade));
                    }

                    reader.Close();

                    string updateQuery = "UPDATE tb_precadastro SET idade = @idade WHERE id_cliente = @idCliente";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                    {
                        foreach (var dado in dadosParaAtualizar)
                        {
                            updateCmd.Parameters.Clear();
                            updateCmd.Parameters.AddWithValue("@idade", dado.idade);
                            updateCmd.Parameters.AddWithValue("@idCliente", dado.idCliente);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }



        private void btnATUALIZARBANCO_Click(object sender, EventArgs e)
        {
            AtualizarTodasAsIdades(); // Atualiza todas as idades no banco de dados
            LoadData(); // Atualizar os dados no DataGridView

        }
        
    }
}
