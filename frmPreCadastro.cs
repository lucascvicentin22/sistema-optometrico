using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaOptometrico
{
    public partial class frmPreCadastro : Form
    {
        private string connectionString = "Server=localhost;Database=db_clinica;User ID=root;Password=2707;";
        public frmPreCadastro()
        {
            InitializeComponent();
            // Configurando o MaskedTextBox para formato de data
            txtNASCIMENTO.Mask = "00/00/0000";
            txtNASCIMENTO.ValidatingType = typeof(DateTime);
        }
        private void ClearFields()
        {
            txtNOMECOMPLETO.Clear();
            txtCPF.Clear();
            txtNASCIMENTO.Clear();
            txtIDADE.Clear();
            txtWHATSAPP.Clear();
            txtRUA.Clear();
            txtCIDADE.Clear();
            txtPROFISSAO.Clear();
            cbESCOLARIDADE.SelectedIndex = -1;
            cbSEXO.SelectedIndex = -1;
        }
        public int CalcularIdade(DateTime dataNascimento)
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento > hoje.AddYears(-idade)) idade--;

            return idade;
        }

        private void btnSALVAR_Click(object sender, EventArgs e)
        {
            // Coleta de dados do formulário
            string nomeCompleto = txtNOMECOMPLETO.Text;
            string cpf = txtCPF.Text;
            DateTime nascimento;
            if (!DateTime.TryParseExact(txtNASCIMENTO.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nascimento))
            {
                MessageBox.Show("Data de nascimento inválida. Por favor, insira no formato dd/MM/yyyy.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idade = int.Parse(txtIDADE.Text); // Certifique-se de que seja um número válido
            string whatsapp = txtWHATSAPP.Text;
            string rua = txtRUA.Text;
            string bairro = txtBAIRRO.Text;
            string cidade = txtCIDADE.Text;
            string profissao = txtPROFISSAO.Text;
            string escolaridade = cbESCOLARIDADE.SelectedItem.ToString();
            string sexo = cbSEXO.SelectedItem.ToString();

            // Comando SQL para inserir os dados na tabela tb_precadastro
            string query = "INSERT INTO tb_precadastro (nome_completo, cpf, nascimento, idade, whatsapp, rua, bairro, cidade, profissao, escolaridade, sexo) " +
                           "VALUES (@nome_completo, @cpf, @nascimento, @idade, @whatsapp,@rua, @bairro, @cidade, @profissao, @escolaridade, @sexo)";

            // Conexão e comando com o banco de dados
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Adicionando parâmetros para prevenir SQL Injection
                        command.Parameters.AddWithValue("@nome_completo", nomeCompleto);
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@nascimento", nascimento.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@idade", idade);
                        command.Parameters.AddWithValue("@whatsapp", whatsapp);
                        command.Parameters.AddWithValue("@rua", rua);
                        command.Parameters.AddWithValue("@bairro", bairro);
                        command.Parameters.AddWithValue("@cidade", cidade);
                        command.Parameters.AddWithValue("@profissao", profissao);
                        command.Parameters.AddWithValue("@escolaridade", escolaridade);
                        command.Parameters.AddWithValue("@sexo", sexo);

                        // Executa o comando de inserção
                        command.ExecuteNonQuery();

                        // Exibe uma mensagem de sucesso
                        MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa os campos do formulário
                        ClearFields();
                    }
                }
                catch (Exception ex)
                {
                    // Em caso de erro, exibe a mensagem de erro
                    MessageBox.Show("Erro ao salvar os dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtNASCIMENTO_TextChanged(object sender, EventArgs e)
        {
            DateTime dataNascimento;

            // Verifica se a data é válida
            if (DateTime.TryParse(txtNASCIMENTO.Text, out dataNascimento))
            {
                int idade = CalcularIdade(dataNascimento);
                txtIDADE.Text = idade.ToString();
            }
            else
            {
                txtIDADE.Text = "Data inválida";
            }
        }
    }
}
